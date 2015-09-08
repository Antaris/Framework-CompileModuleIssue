// <copyright company="Fresh Egg Limited" file="WorkContext.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx
{
    using System.Globalization;
    using Fx.Security;

    /// <summary>
    /// Represents a context for performing work.
    /// </summary>
    public class WorkContext : IWorkContext
    {
        /// <inheritdoc />
        public CultureInfo Culture { get; set; }

        /// <inheritdoc />
        public IIdentity Identity { get; set; }

        /// <inheritdoc />
        public ISecurityContext SecurityContext { get; set; }
    }
}