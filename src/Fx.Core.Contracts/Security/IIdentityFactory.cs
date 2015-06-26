// <copyright company="Fresh Egg Limited" file="IIdentityFactory.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security
{
    using System;

    /// <summary>
    /// Defines the required contract for implementing an identity factory.
    /// </summary>
    public interface IIdentityFactory
    {
        /// <summary>
        /// Creates an anonymous identity.
        /// </summary>
        /// <returns>The anonymous identity.</returns>
        IIdentity CreateAnonymousIdentity();

        /// <summary>
        /// Creates an identity.
        /// </summary>
        /// <param name="id">The id of the identity.</param>
        /// <param name="name">The name of the identity.</param>
        /// <param name="email">The email address associated with the identity.</param>
        /// <param name="isAnonymous">[Optional] Flag to state whether this identity is anonymous.</param>
        /// <param name="isSystem">[Optional] Flag to state whether this identity is a system identity.</param>
        /// <param name="status">[Optional] The status of the identity.</param>
        /// <param name="dateTimeOffset">[Optional] The reference date/time (with offset to UTC) for this identity.</param>
        /// <returns>The identity.</returns>
        IIdentity CreateIdentity(int id, string name, string email, bool isAnonymous = false, bool isSystem = false, IdentityStatus status = IdentityStatus.Offline, DateTimeOffset? dateTimeOffset = null);

        /// <summary>
        /// Creates a system identity.
        /// </summary>
        /// <returns>The system identity.</returns>
        IIdentity CreateSystemIdentity();
    }
}