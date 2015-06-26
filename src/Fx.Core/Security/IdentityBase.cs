// <copyright company="Fresh Egg Limited" file="IdentityBase.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security
{
    using System;

    /// <summary>
    /// Provides a base implementation of an identity.
    /// </summary>
    public abstract class IdentityBase : IIdentity
    {
        /// <summary>
        /// Initialises a new instance of a <see cref="IdentityBase"/>
        /// </summary>
        /// <param name="id">The id of the identity.</param>
        /// <param name="name">The name of the identity.</param>
        /// <param name="email">The email of the identity.</param>
        /// <param name="dateTimeOffset">The date/time (with offset to UTC) reference.</param>
        /// <param name="status">The identity status.</param>
        /// <param name="isAnonymous">Flag to state whether this identity represents an anonymous identity.</param>
        /// <param name="isSystem">Flag to state whether this identity represents a system identity.</param>
        protected IdentityBase(int id, string name, string email, DateTimeOffset dateTimeOffset, IdentityStatus status, bool isAnonymous, bool isSystem)
        {
            Id = id;
            Name = name;
            Email = email;
            DateTimeOffset = dateTimeOffset;
            Status = status;
            IsAnonymous = isAnonymous;
            IsSystem = isSystem;
        }

        /// <inheritdoc />
        public DateTimeOffset DateTimeOffset { get; protected internal set; }

        /// <inheritdoc />
        public string Email { get; protected internal set; }

        /// <inheritdoc />
        public int Id { get; protected internal set; }

        /// <inheritdoc />
        public bool IsAnonymous { get; protected internal set; }

        /// <inheritdoc />
        public bool IsSystem { get; protected internal set; }

        /// <inheritdoc />
        public string Name { get; protected internal set; }

        /// <inheritdoc />
        public IdentityStatus Status { get; protected internal set; }
    }
}