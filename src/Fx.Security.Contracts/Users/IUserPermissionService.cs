// <copyright company="Fresh Egg Limited" file="IUserPermissionService.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security
{
    using System.Linq;
    using Fx.Data;

    /// <summary>
    /// Defines the required contract for implementing a user permission service.
    /// </summary>
    public interface IUserPermissionService : IReader<UserPermission>, IWriter<UserPermission>
    {
        /// <summary>
        /// Gets the available user permission records for the given user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>The user permissions.</returns>
        IQueryable<UserPermission> GetForUser(int userId);
    }
}