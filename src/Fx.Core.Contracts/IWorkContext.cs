// <copyright company="Fresh Egg Limited" file="IWorkContext.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx
{
    using System.Globalization;
    using Fx.Security;

    /// <summary>
    /// Defines the required contract for implementing a work context.
    /// </summary>
    public interface IWorkContext
    {
        /// <summary>
        /// Gets the culture.
        /// </summary>
        CultureInfo Culture { get; }

        /// <summary>
        /// Gets the identity.
        /// </summary>
        IIdentity Identity { get; }

        /// <summary>
        /// Gets the security context.
        /// </summary>
        ISecurityContext SecurityContext { get; }
    }
}