// <copyright company="Fresh Egg Limited" file="ISecurityContextFactory.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security
{
    /// <summary>
    /// Defines the required contract for implementing a security service.
    /// </summary>
    public interface ISecurityService
    {
        /// <summary>
        /// Gets a security context for the current user.
        /// </summary>
        /// <returns>The security context for the current user.</returns>
        ISecurityContext GetCurrentSecurityContext();
    }
}