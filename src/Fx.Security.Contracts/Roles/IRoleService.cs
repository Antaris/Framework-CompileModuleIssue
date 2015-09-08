// <copyright company="Fresh Egg Limited" file="IRoleService.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security
{
    using Fx.Data;

    /// <summary>
    /// Defines the required contract for implementing a role service.
    /// </summary>
    public interface IRoleService : IReader<Role>, IWriter<Role>
    {

    }
}