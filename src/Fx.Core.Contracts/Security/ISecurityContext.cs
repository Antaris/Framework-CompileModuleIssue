// <copyright company="Fresh Egg Limited" file="ISecurityContext.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the required contract for implementing a security context.
    /// </summary>
    public interface ISecurityContext
    {
        /// <summary>
        /// Gets the set of applied permissions.
        /// </summary>
        IEnumerable<Permission> AppliedPermissions { get; }

        /// <summary>
        /// Gets the set of applied dataset permissions.
        /// </summary>
        IEnumerable<DataSetPermission> AppliedDataSetPermissions { get; }

        /*
        /// <summary>
        /// Elevates the security context temporarily with the given permission.
        /// </summary>
        /// <param name="permission">The temporary permission.</param>
        /// <returns>The disposable which reverts the temporary permission.</returns>
        IDisposable Elevate(Permission permission);
        */

        /// <summary>
        /// Determines whether the context has the given dataset permission.
        /// </summary>
        /// <param name="name">The dataset name.</param>
        /// <param name="type">The dataset permission type.</param>
        /// <returns>True if the context has the permission, otherwise false.</returns>
        bool HasDataSetPermission(string name, DataSetPermissionType type);

        /// <summary>
        /// Determines whether the context has the given dataset permission.
        /// </summary>
        /// <param name="name">The dataset.</param>
        /// <param name="type">The dataset permission type.</param>
        /// <returns>True if the context has the permission, otherwise false.</returns>
        bool HasDataSetPermission(DataSet dataset, DataSetPermissionType type);

        /// <summary>
        /// Determines whether the context has the given permission.
        /// </summary>
        /// <param name="code">The permission code.</param>
        /// <returns>True if the context has the permission, otherwise false.</returns>
        bool HasPermission(string code);

        /// <summary>
        /// Determines whether the context has the given permission.
        /// </summary>
        /// <param name="permission">The permission instance.</param>
        /// <returns>True if the context has the permission, otherwise false.</returns>
        bool HasPermission(Permission permission);
    }
}