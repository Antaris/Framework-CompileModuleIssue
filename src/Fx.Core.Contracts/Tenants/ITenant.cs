// <copyright company="Fresh Egg Limited" file="ITenant.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Tenants
{
    /// <summary>
    /// Defines the required contract for implementing a tenant.
    /// </summary>
    public interface ITenant
    {
        /// <summary>
        /// Gets the id of the tenant.
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Gets the tenant code.
        /// </summary>
        string Code { get; }

        /// <summary>
        /// Gets the tenant name.
        /// </summary>
        string Name { get; }
    }
}