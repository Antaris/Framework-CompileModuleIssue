// <copyright company="Fresh Egg Limited" file="IUserDataSetService.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security
{
    using System.Linq;
    using Fx.Data;

    /// <summary>
    /// Defines the required contract for implementing a user permission service.
    /// </summary>
    public interface IUserDataSetService : IReader<UserDataSet>, IWriter<UserDataSet>
    {
        /// <summary>
        /// Gets the available user dataset records for the given user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>The user dataset records.</returns>
        IQueryable<UserDataSet> GetForUser(int userId);
    }
}