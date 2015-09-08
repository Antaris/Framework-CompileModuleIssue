// <copyright company="Fresh Egg Limited" file="RoleService.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security
{
    using Fx.Data;

    /// <summary>
    /// Provides services for managing roles.
    /// </summary>
    public class RoleService : Writer<SecurityDbContext, Role>, IRoleService
    {
        /// <summary>
        /// Initialises a new instance of <see cref="RoleService"/>
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="workContext">The work context.</param>
        /// <param name="clock">The system clock.</param>
        public RoleService(SecurityDbContext context, IWorkContext workContext, IClock clock)
            : base(context, workContext, clock, SecurityDataSets.Roles) {  }
    }
}