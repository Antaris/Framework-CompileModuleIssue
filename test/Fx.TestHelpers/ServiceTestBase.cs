// <copyright company="Fresh Egg Limited" file="ServiceTestBase.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.TestHelpers
{
    using System;
    using Microsoft.Data.Entity;
    using Microsoft.Framework.DependencyInjection;
    using Fx;
    using Fx.Data;


    /// <summary>
    /// Provides services for
    /// </summary>
    public abstract class ServiceTestBase<T, C, E> where T : Writer<C, E> where C : DbContextBase where E : EntityBase
    {
        private IServiceProvider _provider;

        /// <summary>
        /// Initialises a new instance of <see cref="ServiceTestBase{T, C, E}"/>
        /// </summary>
        public ServiceTestBase()
        {
            var services = new ServiceCollection();

            services.AddEntityFramework()
                .AddInMemoryDatabase()
                .AddDbContext<C>(o => o.UseInMemoryDatabase());

            services.AddSingleton<IWorkContext>(p => CreateWorkContext());
            services.AddSingleton<IClock>(p => new Clock());

            services.AddTransient<T>();

            ConfigureServices(services);

            _provider = services.BuildServiceProvider();

            PopulateData(_provider.GetRequiredService<C>());
        }

        /// <summary>
        /// Gets the service provider.
        /// </summary>
        public IServiceProvider ServiceProvider {  get { return _provider; } }

        /// <summary>
        /// Gets the service under test.
        /// </summary>
        public T Service {  get { return _provider.GetRequiredService<T>(); } }

        /// <summary>
        /// Configures any additional services required by the test.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public virtual void ConfigureServices(IServiceCollection services)
        {

        }

        /// <summary>
        /// Creates the work context required for the services.
        /// </summary>
        /// <returns>The work context.</returns>
        public virtual IWorkContext CreateWorkContext()
        {
            return new TestWorkContext();
        }

        /// <summary>
        /// Populates the database context with any test data.
        /// </summary>
        /// <param name="context">The database context.</param>
        public virtual void PopulateData(C context)
        {

        }
    }
}