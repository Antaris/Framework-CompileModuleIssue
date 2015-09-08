// <copyright company="Fresh Egg Limited" file="Host.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Data.Entity;
    using Microsoft.Data.Entity.Infrastructure;
    using Microsoft.Framework.Configuration;
    using Microsoft.Framework.DependencyInjection;
    using Microsoft.Framework.DependencyInjection.Extensions;
    using Fx.Data;
    using Fx.Security;

    /// <summary>
    /// Provides host services for bootstrapping Fx-based applications.
    /// </summary>
    public class Host<T> : IHost where T : IModule
    {
        private readonly IModuleProvider _moduleProvider;
        private readonly IConfiguration _configuration;
        private Func<EntityFrameworkServicesBuilder, EntityFrameworkServicesBuilder> _dataBuilder;
        private Action<IConnectionStringFactory, DbContextOptionsBuilder> _optionsAction;
        private IServiceProvider _rootServiceProvider;
        
        private readonly IModule[] _modules;
        private readonly IDataModule[] _dataModules;

        private readonly DataSet[] _datasets;
        private readonly Permission[] _permissions;
        private readonly PermissionConvention[] _permissionConventions;

        private readonly IModule _hostModule;

        /// <summary>
        /// Initialises a new instance of <see cref=""/>
        /// </summary>
        /// <param name="moduleProvider">The module provider.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="dataBuilder">The data configuration builder.</param>
        public Host(IModuleProvider moduleProvider, IConfiguration configuration)
        {
            _moduleProvider = Ensure.ArgumentNotNull(moduleProvider, nameof(moduleProvider));
            _configuration = Ensure.ArgumentNotNull(configuration, nameof(configuration));

            _modules = moduleProvider.GetModules().ToArray();
            _dataModules = _modules.OfType<IDataModule>().ToArray();

            _hostModule = _modules.SingleOrDefault(m => m is T);
            if (_hostModule == null)
            {
                throw new InvalidOperationException($"Host module {typeof(T).Name} was not found by the provider.");
            }

            _datasets = _modules.SelectMany(m => m.GetDataSets()).ToArray();
            _permissions = _modules.SelectMany(m => m.GetPermissions()).ToArray();
            _permissionConventions = FlattenAvailablePermissionConventions(_modules.SelectMany(m => m.GetPermissionConventions())).ToArray();
        }

        /// <summary>
        /// Sets the delegates used to configure entity framework integration.
        /// </summary>
        /// <param name="dataBuilder">The data builder.</param>
        /// <param name="optionAction">[Optional] The action used to configure per context options.</param>
        public void ConfigureEntityFramework(Func<EntityFrameworkServicesBuilder, EntityFrameworkServicesBuilder> dataBuilder, Action<IConnectionStringFactory, DbContextOptionsBuilder> optionAction = null)
        {
            _dataBuilder = Ensure.ArgumentNotNull(dataBuilder, nameof(dataBuilder));
            _optionsAction = optionAction;
        }

        /// <summary>
        /// Configures all available services.
        /// </summary>
        /// <param name="services">The services collection.</param>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            Ensure.ArgumentNotNull(services, nameof(services));

            // Add the host.
            services.AddSingleton<IHost>(p => this);

            // Add the configuration.
            services.AddSingleton(p => _configuration);

            // Add the host module.
            services.AddSingleton(p => _hostModule);

            // Wire up all the required services from the modules.
            foreach (var module in _modules)
            {
                foreach (var descriptor in module.GetServiceDescriptors())
                {
                    // We use replace in case an instance has been registered previously.
                    services.Replace(descriptor);
                }
            }

            if (_dataBuilder != null)
            {
                var entityFrameworkServices = services.AddEntityFramework();
                entityFrameworkServices = _dataBuilder(entityFrameworkServices);

                // Wire up all the data related services.
                foreach (var module in _dataModules)
                {
                    module.ConfigureDbContext(entityFrameworkServices, b =>
                    {
                        if (_optionsAction != null)
                        {
                            var connectionStringFactory = _rootServiceProvider.GetRequiredService<IConnectionStringFactory>();
                            _optionsAction(connectionStringFactory, b);
                        }
                    });
                }
            }

            _rootServiceProvider = services.BuildServiceProvider();

            return _rootServiceProvider;
        }

        /// <inheritdoc />
        public IEnumerable<IModule> GetAvailableModules()
        {
            return _modules;
        }

        /// <inheritdoc />
        public IEnumerable<DataSet> GetAvailableDataSets()
        {
            return _datasets;
        }

        /// <inheritdoc />
        public IEnumerable<Permission> GetAvailablePermissions()
        {
            return _permissions;
        }

        /// <inheritdoc />
        public IEnumerable<PermissionConvention> GetAvailablePermissionConventions()
        {
            return _permissionConventions;
        }

        /// <summary>
        /// Flattens the set of permission conventions into the amalgamated set of permission conventions, grouped by code.
        /// </summary>
        /// <param name="conventions">The set of available conventions.</param>
        /// <returns>The set of amalgamated permission conventions.</returns>
        private IEnumerable<PermissionConvention> FlattenAvailablePermissionConventions(IEnumerable<PermissionConvention> conventions)
        {
            // This takes all available conventions and amalgamates them into distinct permission conventions.
            return conventions.GroupBy(c => c.Code)
                .Select(g =>
                {
                    var first = g.First();

                    return new PermissionConvention(first.Code, first.Name, g.SelectMany(pc => pc.Permissions).ToArray());
                });
        }
    }
}