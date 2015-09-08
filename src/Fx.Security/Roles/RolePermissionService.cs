// <copyright company="Fresh Egg Limited" file="RolePermissionService.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security
{
    using System.Linq;
    using Fx.Data;

    /// <summary>
    /// Provides services for managing role permissions.
    /// </summary>
    public class RolePermissionService : Writer<SecurityDbContext, RolePermission>, IRolePermissionService
    {
        /// <summary>
        /// Initialises a new instance of <see cref="RolePermissionService"/>
        /// </summary>
        /// <param name="context">The security database context.</param>
        /// <param name="workContext">The work context.</param>
        /// <param name="clock">The system clock.</param>
        public RolePermissionService(SecurityDbContext context, IWorkContext workContext, IClock clock)
            : base(context, workContext, clock, SecurityDataSets.RolePermissions)
        {  }

        /// <inheritdoc />
        public IQueryable<RolePermission> GetForRole(int roleId)
        {
            return GetAll(rp => rp.RoleId == roleId);
        }
    }
}