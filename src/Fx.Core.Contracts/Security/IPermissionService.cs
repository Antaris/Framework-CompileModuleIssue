// <copyright company="Fresh Egg Limited" file="IPermissionService.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the required contract for implementing a permission service.
    /// </summary>
    public interface IPermissionService
    {
        /// <summary>
        /// Gets the available permission conventions.
        /// </summary>
        /// <returns>The set of available permission conventions.</returns>
        IEnumerable<PermissionConvention> GetAvailablePermissionConventions();

        /// <summary>
        /// Gets the available permissions.
        /// </summary>
        /// <returns>The set of available permissions.</returns>
        IEnumerable<Permission> GetAvailablePermissions();
    }
}