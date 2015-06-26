// <copyright company="Fresh Egg Limited" file="IdentityStatus.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security
{
    /// <summary>
    /// Defines the possible identity statuses.
    /// </summary>
    public enum IdentityStatus
    {
        /// <summary>
        /// The identity is online.
        /// </summary>
        Online,

        /// <summary>
        /// The identity is offline.
        /// </summary>
        Offline,

        /// <summary>
        /// The identity is unavailable (it may have been deleted, or may be a system identity).
        /// </summary>
        Unavailable,

        /// <summary>
        /// The identity is busy/away.
        /// </summary>
        Busy
    }
}