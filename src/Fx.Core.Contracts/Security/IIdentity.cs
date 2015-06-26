// <copyright company="Fresh Egg Limited" file="IIdentity.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security
{
    using System;

    /// <summary>
    /// Defines the required contract for implementing an identity.
    /// </summary>
    public interface IIdentity
    {
        /// <summary>
        /// Gets the reference date/time (with offset to UTC) for this identity.
        /// </summary>
        /// <remarks>
        /// Depending on the use-case for an identity, this could represent a login date (for authentication), a create/update date (for CRUD operations), etc.
        /// </remarks>
        DateTimeOffset DateTimeOffset { get; }

        /// <summary>
        /// Gets the email.
        /// </summary>
        string Email { get; }

        /// <summary>
        /// Gets the id.
        /// </summary>
        int Id { get; }

        /// <summary>
        /// Gets whether this is an anonymous identity.
        /// </summary>
        bool IsAnonymous { get; }

        /// <summary>
        /// Gets whether this is a system identity.
        /// </summary>
        bool IsSystem { get; }

        /// <summary>
        /// Gets the name of the identity.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the status of the identity.
        /// </summary>
        IdentityStatus Status { get; }
    }
}