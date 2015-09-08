// <copyright company="Fresh Egg Limited" file="IdentityFactory.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security
{
    using System;

    /// <summary>
    /// Represents an identity factory.
    /// </summary>
    public class IdentityFactory : IIdentityFactory
    {
        private readonly IClock _clock;

        /// <summary>
        /// Initialises a new instance of <see cref="IdentityFactory"/>
        /// </summary>
        /// <param name="clock">The clock.</param>
        public IdentityFactory(IClock clock)
        {
            _clock = clock;
        }

        /// <inheritdoc />
        public virtual IIdentity CreateAnonymousIdentity()
        {
            return new AnonymousIdentity(_clock.UtcNow);
        }

        /// <inheritdoc />
        public virtual IIdentity CreateIdentity(int id, string name, string email, bool isAnonymous = false, bool isSystem = false, IdentityStatus status = IdentityStatus.Offline, DateTimeOffset? dateTimeOffset = null)
        {
            return new Identity(id, name, email, dateTimeOffset.GetValueOrDefault(_clock.UtcNow), status, isAnonymous, isSystem);
        }

        /// <inheritdoc />
        public virtual IIdentity CreateSystemIdentity()
        {
            return new SystemIdentity(_clock.UtcNow);
        }
    }
}