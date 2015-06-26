﻿// <copyright company="Fresh Egg Limited" file="Identity.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security
{
    using System;

    /// <summary>
    /// Represents an identity.
    /// </summary>
    public class Identity : IdentityBase
    {
        /// <summary>
        /// Initialises a new instance of <see cref="Identity"/>
        /// </summary>
        /// <param name="id">The id of the identity.</param>
        /// <param name="name">The name of the identity.</param>
        /// <param name="email">The email of the identity.</param>
        /// <param name="dateTimeOffset">The date/time (with offset to UTC) reference.</param>
        /// <param name="status">The identity status.</param>
        public Identity(int id, string name, string email, DateTimeOffset dateTimeOffset, IdentityStatus status)
            : base(id, name, email, dateTimeOffset, status, false, false)
        { }
    }
}