// <copyright company="Fresh Egg Limited" file="UserService.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security
{
    using System.Linq;
    using Fx.Data;

    /// <summary>
    /// Provides services for managing users.
    /// </summary>
    public class UserService : Writer<SecurityDbContext, User>, IUserService
    {
        /// <summary>
        /// Initialises a new instance of <see cref="UserService"/>
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="workContext">The work context.</param>
        /// <param name="clock">The system clock.</param>
        public UserService(SecurityDbContext context, IWorkContext workContext, IClock clock)
            : base(context, workContext, clock, SecurityDataSets.Users) {  }

        /// <inheritdoc />
        public bool Exists(string username)
        {
            Ensure.ArgumentNotNullOrWhiteSpace(username, nameof(username));

            return Exists(u => u.Username == username);
        }

        /// <inheritdoc />
        public IQueryable<User> GetAllInRole(int roleId)
        {
            return GetAll(u => u.Roles.Any(r => r.Id == roleId));
        }
    }
}