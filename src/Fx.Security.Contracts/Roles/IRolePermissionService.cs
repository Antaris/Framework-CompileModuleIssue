// <copyright company="Fresh Egg Limited" file="IRolePermissionService.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security
{
    using System.Linq;
    using Fx.Data;

    /// <summary>
    /// Defines the required contract for implementing a role permission service.
    /// </summary>
    public interface IRolePermissionService : IReader<RolePermission>, IWriter<RolePermission>
    {
        /// <summary>
        /// Gets the available role permission records for the given role.
        /// </summary>
        /// <param name="roleId">The role id.</param>
        /// <returns>The role permissions.</returns>
        IQueryable<RolePermission> GetForRole(int roleId);
    }
}