// <copyright company="Fresh Egg Limited" file="DefaultIdentityService.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security
{
    using System;

    /// <summary>
    /// Represents the default identity service.
    /// </summary>
    public class DefaultIdentityService : IIdentityService
    {
        private readonly IIdentityFactory _factory;

        /// <summary>
        /// Initialises a new instance of <see cref="DefaultIdentityService"/>.
        /// </summary>
        /// <param name="factory">The identity factory.</param>
        public DefaultIdentityService(IIdentityFactory factory)
        {
            _factory = Ensure.ArgumentNotNull(factory, nameof(factory));
        }

        /// <inheritdoc />
        public IIdentity GetForCurrent(DateTimeOffset? referenceDataTime = default(DateTimeOffset?))
        {
            return _factory.CreateAnonymousIdentity();
        }

        /// <inheritdoc />
        public IIdentity GetForId(int id, DateTimeOffset? referenceDateTime = default(DateTimeOffset?))
        {
            return null;
        }

        /// <inheritdoc />
        public void SetIdentityStatus(int id, IdentityStatus status) {  }
    }
}