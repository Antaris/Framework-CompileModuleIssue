// <copyright company="Fresh Egg Limited" file="IIdentityService.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security
{
    using System;

    /// <summary>
    /// Defines the required contract for implementing an identity service.
    /// </summary>
    public interface IIdentityService
    {
        /// <summary>
        /// Gets the identity of the current logged in user.
        /// </summary>
        /// <param name="referenceDataTime">[Optional] The reference date/time.</param>
        /// <returns>The identity.</returns>
        IIdentity GetForCurrent(DateTimeOffset? referenceDataTime = null);

        /// <summary>
        /// Gets the identity for the given Id.
        /// </summary>
        /// <param name="id">The id of the identity.</param>
        /// <param name="referenceDateTime">[Optional] The reference date/time.</param>
        /// <returns>The identity.</returns>
        IIdentity GetForId(int id, DateTimeOffset? referenceDateTime = null);

        /// <summary>
        /// Sets the identity status for the identity with the given id.
        /// </summary>
        /// <param name="id">The id of the identity.</param>
        /// <param name="status">The status of the identity.</param>
        void SetIdentityStatus(int id, IdentityStatus status);
    }
}