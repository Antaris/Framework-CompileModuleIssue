// <copyright company="Fresh Egg Limited" file="SystemIdentity.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security
{
    using System;

    /// <summary>
    /// Represents a system identity.
    /// </summary>
    public sealed class SystemIdentity : IdentityBase
    {
        /// <summary>
        /// Initialises a new instance of <see cref="SystemIdentity"/>
        /// </summary>
        /// <param name="dateTimeOffset">The date/time offset to UTC.</param>
        internal SystemIdentity(DateTimeOffset dateTimeOffset)
            : base(0, "System", null, dateTimeOffset, IdentityStatus.Unavailable, false, true)
        {

        }
    }
}