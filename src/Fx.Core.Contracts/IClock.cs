// <copyright company="Fresh Egg Limited" file="IClock.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx
{
    using System;

    /// <summary>
    /// Defines the required contract for implementing a clock.
    /// </summary>
    public interface IClock
    {
        /// <summary>
        /// Gets current the date/time (with offset to UTC)
        /// </summary>
        DateTimeOffset UtcNow { get; }
    }
}