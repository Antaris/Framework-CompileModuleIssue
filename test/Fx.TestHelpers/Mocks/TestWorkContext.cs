// <copyright company="Fresh Egg Limited" file="TestWorkContext.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.TestHelpers
{
    using System;
    using System.Globalization;
    using System.Threading;
    using Fx;
    using Fx.Security;

    /// <summary>
    /// Provides a test work context.
    /// </summary>
    public class TestWorkContext : IWorkContext
    {
        /// <summary>
        /// Initialises a new instance of <see cref="TestWorkContext"/>
        /// </summary>
        public TestWorkContext()
        {
            Culture = Thread.CurrentThread.CurrentUICulture;
            Identity = new AnonymousIdentity(DateTimeOffset.Now);
            SecurityContext = new SecurityContextBuilder().Build();
        }

        /// <inheritdoc />
        public CultureInfo Culture { get; set; }

        /// <inheritdoc />
        public IIdentity Identity { get; set; }

        /// <inheritdoc />
        public ISecurityContext SecurityContext { get; set; }
    }
}