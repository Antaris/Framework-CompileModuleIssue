// <copyright company="Fresh Egg Limited" file="ITenantContext.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Tenants
{
    /// <summary>
    /// Defines the required contract for implementing a tenant context.
    /// </summary>
    public interface ITenantContext
    {
        /// <summary>
        /// Gets the tenant.
        /// </summary>
        ITenant Tenant { get; }
    }
}