// <copyright company="Fresh Egg Limited" file="UserDataSetService.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security
{
    using System.Linq;
    using Fx.Data;

    /// <summary>
    /// Provides services for managing user permissions.
    /// </summary>
    public class UserDataSetService : Writer<SecurityDbContext, UserDataSet>, IUserDataSetService
    {
        /// <summary>
        /// Initialises a new instance of <see cref="UserDataSetService"/>
        /// </summary>
        /// <param name="context">The security database context.</param>
        /// <param name="workContext">The work context.</param>
        /// <param name="clock">The system clock.</param>
        public UserDataSetService(SecurityDbContext context, IWorkContext workContext, IClock clock)
            : base(context, workContext, clock, SecurityDataSets.UserDataSets)
        {  }

        /// <inheritdoc />
        public IQueryable<UserDataSet> GetForUser(int userId)
        {
            return GetAll(ud => ud.UserId == userId);
        }
    }
}