// <copyright company="Fresh Egg Limited" file="AnonymousIdentity.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security
{
    using System;

    /// <summary>
    /// Represents an anonymous identity.
    /// </summary>
    public sealed class AnonymousIdentity : IdentityBase
    {
        /// <summary>
        /// Initialises a new instance of <see cref="AnonymousIdentity"/>
        /// </summary>
        /// <param name="dateTimeOffset">The date/time offset to UTC.</param>
        public AnonymousIdentity(DateTimeOffset dateTimeOffset) 
            : base(-1, "Guest", null, dateTimeOffset, IdentityStatus.Unavailable, true, false)
        {

        }
    }
}