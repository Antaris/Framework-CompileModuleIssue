// <copyright company="Fresh Egg Limited" file="ILoggerFactory.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Logging
{
    /// <summary>
    /// Defines the required contract for implementing a logger factory.
    /// </summary>
    public interface ILoggerFactory
    {
        /// <summary>
        /// Creates the logger.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="parent">The parent logger.</param>
        /// <returns>The logger with the specified name.</returns>
        ILogger CreateLogger(string name = null, ILogger parent = null);
    }
}