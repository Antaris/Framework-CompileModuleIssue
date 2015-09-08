// <copyright company="Fresh Egg Limited" file="ModuleBase.cs">
// Copyright © 2014 Fresh Egg Limited
// </copyright>

namespace Fx
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Microsoft.Framework.DependencyInjection;
    using Fx.Security;

    /// <summary>
    /// Provides a base implementation of a module.
    /// </summary>
    public abstract class ModuleBase : IModule
    {
        private readonly Lazy<SemanticVersion> _versionThunk;
        private readonly Type _type;
        private readonly Assembly _assembly;

        /// <summary>
        /// Initialises a new instance of <see cref="ModuleBase"/>
        /// </summary>
        protected ModuleBase()
        {
            _type = GetType();
            _assembly = _type.Assembly;
            
            _versionThunk = new Lazy<SemanticVersion>(ResolveVersion);
        }
        
        /// <inheritdoc />
        public virtual string Name
        {
            get { return _type.Name.Replace("Module", ""); }
        }

        /// <inheritdoc />
        public virtual int Order
        {
            get { return ModuleOrder.Level4; }
        }

        /// <inheritdoc />
        public virtual SemanticVersion Version
        {
            get { return _versionThunk.Value; }
        }

        /// <inheritdoc />
        public virtual IEnumerable<DataSet> GetDataSets()
        {
            yield break;
        }

        /// <inheritdoc />
        public virtual IEnumerable<Permission> GetPermissions()
        {
            yield break;
        }

        /// <inheritdoc />
        public virtual IEnumerable<PermissionConvention> GetPermissionConventions()
        {
            yield break;
        }

        /// <inheritdoc />
        public virtual IEnumerable<ServiceDescriptor> GetServiceDescriptors()
        {
            yield break;
        }

        /// <summary>
        /// Resolves the version of this module.
        /// </summary>
        /// <returns>The semantic version.</returns>
        private SemanticVersion ResolveVersion()
        {
            SemanticVersion semver;
            var versionAttribute = _assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
            if (versionAttribute != null && SemanticVersion.TryParse(versionAttribute.InformationalVersion, out semver))
            {
                return semver;
            }

            return new SemanticVersion(0);
        }
    }
}