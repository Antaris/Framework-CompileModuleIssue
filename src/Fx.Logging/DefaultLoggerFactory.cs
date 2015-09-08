// <copyright company="Fresh Egg Limited" file="DefaultLoggerFactory.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Logging
{
    /// <summary>
    /// Generates logger instances that perform no operations.
    /// </summary>
    public class DefaultLoggerFactory : ILoggerFactory
    {
        /// <inheritdoc />
        public ILogger CreateLogger(string name = null, ILogger parent = null)
        {
            return new DefaultLogger();
        }
    }
}