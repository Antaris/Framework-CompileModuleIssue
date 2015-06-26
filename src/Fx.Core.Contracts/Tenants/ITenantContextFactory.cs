// <copyright company="Fresh Egg Limited" file="ITenantContextFactory.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Tenants
{
    /// <summary>
    /// Defines the required contract for implementing a tenant context factory.
    /// </summary>
    public interface ITenantContextFactory
    {
        /// <summary>
        /// Creates the tenant context.
        /// </summary>
        /// <returns>The tenant context.</returns>
        ITenantContext CreateTenantContext();
    }
}