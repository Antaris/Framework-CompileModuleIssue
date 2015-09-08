// <copyright company="Fresh Egg Limited" file="UserPermissionService.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security
{
    using System.Linq;
    using Fx.Data;

    /// <summary>
    /// Provides services for managing user permissions.
    /// </summary>
    public class UserPermissionService : Writer<SecurityDbContext, UserPermission>, IUserPermissionService
    {
        /// <summary>
        /// Initialises a new instance of <see cref="UserPermissionService"/>
        /// </summary>
        /// <param name="context">The security database context.</param>
        /// <param name="workContext">The work context.</param>
        /// <param name="clock">The system clock.</param>
        public UserPermissionService(SecurityDbContext context, IWorkContext workContext, IClock clock)
            : base(context, workContext, clock, SecurityDataSets.UserPermissions)
        {  }

        /// <inheritdoc />
        public IQueryable<UserPermission> GetForUser(int userId)
        {
            return GetAll(up => up.UserId == userId);
        }
    }
}