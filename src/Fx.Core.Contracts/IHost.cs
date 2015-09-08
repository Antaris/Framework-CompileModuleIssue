// <copyright company="Fresh Egg Limited" file="IHost.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx
{
    using System.Collections.Generic;
    using Fx.Security;

    /// <summary>
    /// Defines the required contract for implementing a host.
    /// </summary>
    public interface IHost
    {
        /// <summary>
        /// Gets all modules available to the host.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IModule> GetAvailableModules();

        /// <summary>
        /// Gets all datasets available to the host.
        /// </summary>
        /// <returns></returns>
        IEnumerable<DataSet> GetAvailableDataSets();

        /// <summary>
        /// Gets all permissions available to the host.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Permission> GetAvailablePermissions();

        /// <summary>
        /// Gets all permission conventions available to the host.
        /// </summary>
        /// <returns></returns>
        IEnumerable<PermissionConvention> GetAvailablePermissionConventions();
    }
}