// <copyright company="Fresh Egg Limited" file="LogRequest.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Logging
{
    using System;

    /// <summary>
    /// Represents a request to log information.
    /// </summary>
    public class LogRequest
    {
        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        public Level Level { get; set; }

        /// <summary>
        /// Gets or sets the message factory.
        /// </summary>
        public Func<string> MessageFactory { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
    }
}