// <copyright company="Fresh Egg Limited" file="IRoleDataSetService.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security
{
    using System.Linq;
    using Fx.Data;

    /// <summary>
    /// Defines the required contract for implementing a role dataset service.
    /// </summary>
    public interface IRoleDataSetService : IReader<RoleDataSet>, IWriter<RoleDataSet>
    {
        /// <summary>
        /// Gets the available role dataset records for the given role.
        /// </summary>
        /// <param name="roleId">The role id.</param>
        /// <returns>The role dataset records.</returns>
        IQueryable<RoleDataSet> GetForRole(int roleId);
    }
}