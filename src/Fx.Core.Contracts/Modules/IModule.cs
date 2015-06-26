// <copyright company="Fresh Egg Limited" file="IModule.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Framework.DependencyInjection;
    using Fx.Security;

    /// <summary>
    /// Defines the required contract for implementing a module.
    /// </summary>
    public interface IModule
    {
        /// <summary>
        /// Gets the name of the module.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the semantic version of the module.
        /// </summary>
        SemanticVersion Version { get; }

        /// <summary>
        /// Gets the set of permissions provided by the module.
        /// </summary>
        /// <returns>The set of permissions.</returns>
        IEnumerable<Permission> GetPermissions();

        /// <summary>
        /// Gets the set of permission conventions provided by the module.
        /// </summary>
        /// <returns>The set of permission conventions.</returns>
        IEnumerable<PermissionConvention> GetPermissionConventions();

        /// <summary>
        /// Gets the set of service descriptors.
        /// </summary>
        /// <returns>The set of service descriptors.</returns>
        IEnumerable<ServiceDescriptor> GetServiceDescriptors();
    }
}