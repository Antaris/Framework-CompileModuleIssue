// <copyright company="Fresh Egg Limited" file="DefaultLogger.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Logging
{
    /// <summary>
    /// Represents a logger that performs no operations.
    /// </summary>
    public class DefaultLogger : LoggerBase
    {
        /// <summary>
        /// Initialises a new instance of <see cref="DefaultLogger"/>
        /// </summary>
        public DefaultLogger()
            : base(null, "NoOp", null)
        { }

        /// <inheritdoc />
        protected override void LogInternal(LogRequest request)
        {
            // NOOP
        }
    }
}