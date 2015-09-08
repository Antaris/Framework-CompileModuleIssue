// <copyright company="Fresh Egg Limited" file="IUserService.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security
{
    using System.Linq;
    using Fx.Data;

    /// <summary>
    /// Defines the required contract for implementing a user service.
    /// </summary>
    public interface IUserService : IReader<User>, IWriter<User>
    {
        /// <summary>
        /// Determines if a user exists with the given username.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>True if the user exists, otherwise false.</returns>
        bool Exists(string username);

        /// <summary>
        /// Gets the set of users in the given role.
        /// </summary>
        /// <param name="roleId">The role id.</param>
        /// <returns>The set of users.</returns>
        IQueryable<User> GetAllInRole(int roleId);
    }
}