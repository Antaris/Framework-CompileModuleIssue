// <copyright company="Fresh Egg Limited" file="RoleDataSetService.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security
{
    using System.Linq;
    using Fx.Data;

    /// <summary>
    /// Provides services for managing user permissions.
    /// </summary>
    public class RoleDataSetService : Writer<SecurityDbContext, RoleDataSet>, IRoleDataSetService
    {
        /// <summary>
        /// Initialises a new instance of <see cref="RoleDataSetService"/>
        /// </summary>
        /// <param name="context">The security database context.</param>
        /// <param name="workContext">The work context.</param>
        /// <param name="clock">The system clock.</param>
        public RoleDataSetService(SecurityDbContext context, IWorkContext workContext, IClock clock)
            : base(context, workContext, clock, SecurityDataSets.RoleDataSets)
        {  }

        /// <inheritdoc />
        public IQueryable<RoleDataSet> GetForRole(int roleId)
        {
            return GetAll(rd => rd.RoleId == roleId);
        }
    }
}