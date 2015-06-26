// <copyright company="Fresh Egg Limited" file="LoggerBase.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Logging
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Represents a base implementation of a logger.
    /// </summary>
    public abstract class LoggerBase : ILogger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoggerBase"/> class.
        /// </summary>
        /// <param name="factory">The logger factory.</param>
        /// <param name="name">The name.</param>
        /// <param name="parent">The parent logger.</param>
        protected LoggerBase(ILoggerFactory factory, string name, ILogger parent = null)
        {
            Factory = factory;
            Name = name;
            Parent = parent;
        }

        /// <summary>
        /// Gets the logger factory.
        /// </summary>
        public virtual ILoggerFactory Factory { get; private set; }

        /// <inheritdoc />
        public virtual string Name { get; private set; }

        /// <summary>
        /// Gets the parent logger.
        /// </summary>
        public virtual ILogger Parent { get; private set; }

        /// <inheritdoc />
        public virtual ILogger CreateChildLogger(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("A logger name is expected.");
            }

            name = string.IsNullOrWhiteSpace(Name) ? name : Name + "." + name;

            return Factory.CreateLogger(name, this);
        }

        /// <inheritdoc />
        public virtual void Debug(string message)
        {
            LogInternal(CreateLogRequest(Level.Debug, message, null));
        }

        /// <inheritdoc />
        public virtual void Debug(Exception exception)
        {
            LogInternal(CreateLogRequest(Level.Debug, (string)null, exception));
        }

        /// <inheritdoc />
        public virtual void Debug(Func<string> messageFactory)
        {
            LogInternal(CreateLogRequest(Level.Debug, messageFactory, null));
        }

        /// <inheritdoc />
        public virtual void Debug(string message, Exception exception)
        {
            LogInternal(CreateLogRequest(Level.Debug, message, exception));
        }

        /// <inheritdoc />
        public virtual void DebugFormat(string format, params object[] args)
        {
            LogInternal(CreateLogRequest(Level.Debug, null, null, format, args));
        }

        /// <inheritdoc />
        public virtual void DebugFormat(Exception exception, string format, params object[] args)
        {
            LogInternal(CreateLogRequest(Level.Debug, exception, null, format, args));
        }

        /// <inheritdoc />
        public virtual void DebugFormat(IFormatProvider provider, string format, params object[] args)
        {
            LogInternal(CreateLogRequest(Level.Debug, null, provider, format, args));
        }

        /// <inheritdoc />
        public virtual void DebugFormat(Exception exception, IFormatProvider provider, string format, params object[] args)
        {
            LogInternal(CreateLogRequest(Level.Debug, exception, provider, format, args));
        }

        /// <inheritdoc />
        public virtual void Error(string message)
        {
            LogInternal(CreateLogRequest(Level.Error, message, null));
        }

        /// <inheritdoc />
        public virtual void Error(Exception exception)
        {
            LogInternal(CreateLogRequest(Level.Error, (string)null, exception));
        }

        /// <inheritdoc />
        public virtual void Error(Func<string> messageFactory)
        {
            LogInternal(CreateLogRequest(Level.Error, messageFactory, null));
        }

        /// <inheritdoc />
        public virtual void Error(string message, Exception exception)
        {
            LogInternal(CreateLogRequest(Level.Error, message, exception));
        }

        /// <inheritdoc />
        public virtual void ErrorFormat(string format, params object[] args)
        {
            LogInternal(CreateLogRequest(Level.Error, null, null, format, args));
        }

        /// <inheritdoc />
        public virtual void ErrorFormat(Exception exception, string format, params object[] args)
        {
            LogInternal(CreateLogRequest(Level.Error, exception, null, format, args));
        }

        /// <inheritdoc />
        public virtual void ErrorFormat(IFormatProvider provider, string format, params object[] args)
        {
            LogInternal(CreateLogRequest(Level.Error, null, provider, format, args));
        }

        /// <inheritdoc />
        public virtual void ErrorFormat(Exception exception, IFormatProvider provider, string format, params object[] args)
        {
            LogInternal(CreateLogRequest(Level.Error, exception, provider, format, args));
        }

        /// <inheritdoc />
        public virtual void Fatal(string message)
        {
            LogInternal(CreateLogRequest(Level.Fatal, message, null));
        }

        /// <inheritdoc />
        public virtual void Fatal(Exception exception)
        {
            LogInternal(CreateLogRequest(Level.Fatal, (string)null, exception));
        }

        /// <inheritdoc />
        public virtual void Fatal(Func<string> messageFactory)
        {
            LogInternal(CreateLogRequest(Level.Fatal, messageFactory, null));
        }

        /// <inheritdoc />
        public virtual void Fatal(string message, Exception exception)
        {
            LogInternal(CreateLogRequest(Level.Fatal, message, exception));
        }

        /// <inheritdoc />
        public virtual void FatalFormat(string format, params object[] args)
        {
            LogInternal(CreateLogRequest(Level.Fatal, null, null, format, args));
        }

        /// <inheritdoc />
        public virtual void FatalFormat(Exception exception, string format, params object[] args)
        {
            LogInternal(CreateLogRequest(Level.Fatal, exception, null, format, args));
        }

        /// <inheritdoc />
        public virtual void FatalFormat(IFormatProvider provider, string format, params object[] args)
        {
            LogInternal(CreateLogRequest(Level.Fatal, null, provider, format, args));
        }

        /// <inheritdoc />
        public virtual void FatalFormat(Exception exception, IFormatProvider provider, string format, params object[] args)
        {
            LogInternal(CreateLogRequest(Level.Fatal, exception, provider, format, args));
        }

        /// <inheritdoc />
        public virtual void Info(string message)
        {
            LogInternal(CreateLogRequest(Level.Info, message, null));
        }

        /// <inheritdoc />
        public virtual void Info(Exception exception)
        {
            LogInternal(CreateLogRequest(Level.Info, (string)null, exception));
        }

        /// <inheritdoc />
        public virtual void Info(Func<string> messageFactory)
        {
            LogInternal(CreateLogRequest(Level.Info, messageFactory, null));
        }

        /// <inheritdoc />
        public virtual void Info(string message, Exception exception)
        {
            LogInternal(CreateLogRequest(Level.Info, message, exception));
        }

        /// <inheritdoc />
        public virtual void InfoFormat(string format, params object[] args)
        {
            LogInternal(CreateLogRequest(Level.Info, null, null, format, args));
        }

        /// <inheritdoc />
        public virtual void InfoFormat(Exception exception, string format, params object[] args)
        {
            LogInternal(CreateLogRequest(Level.Info, exception, null, format, args));
        }

        /// <inheritdoc />
        public virtual void InfoFormat(IFormatProvider provider, string format, params object[] args)
        {
            LogInternal(CreateLogRequest(Level.Info, null, provider, format, args));
        }

        /// <inheritdoc />
        public virtual void InfoFormat(Exception exception, IFormatProvider provider, string format, params object[] args)
        {
            LogInternal(CreateLogRequest(Level.Info, exception, provider, format, args));
        }

        /// <inheritdoc />
        public virtual void Warning(string message)
        {
            LogInternal(CreateLogRequest(Level.Warning, message, null));
        }

        /// <inheritdoc />
        public virtual void Warning(Exception exception)
        {
            LogInternal(CreateLogRequest(Level.Warning, (string)null, exception));
        }

        /// <inheritdoc />
        public virtual void Warning(Func<string> messageFactory)
        {
            LogInternal(CreateLogRequest(Level.Warning, messageFactory, null));
        }

        /// <inheritdoc />
        public virtual void Warning(string message, Exception exception)
        {
            LogInternal(CreateLogRequest(Level.Warning, message, exception));
        }

        /// <inheritdoc />
        public virtual void WarningFormat(string format, params object[] args)
        {
            LogInternal(CreateLogRequest(Level.Warning, null, null, format, args));
        }

        /// <inheritdoc />
        public virtual void WarningFormat(Exception exception, string format, params object[] args)
        {
            LogInternal(CreateLogRequest(Level.Warning, exception, null, format, args));
        }

        /// <inheritdoc />
        public virtual void WarningFormat(IFormatProvider provider, string format, params object[] args)
        {
            LogInternal(CreateLogRequest(Level.Warning, null, provider, format, args));
        }

        /// <inheritdoc />
        public virtual void WarningFormat(Exception exception, IFormatProvider provider, string format, params object[] args)
        {
            LogInternal(CreateLogRequest(Level.Warning, exception, provider, format, args));
        }

        /// <summary>
        /// Processes the log request.
        /// </summary>
        /// <param name="request">The log request.</param>
        protected internal abstract void LogInternal(LogRequest request);

        /// <summary>
        /// Creates the log request.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        private LogRequest CreateLogRequest(Level level, string message, Exception exception)
        {
            return new LogRequest()
            {
                Name = Name,
                Exception = exception,
                Level = level,
                MessageFactory = () => message
            };
        }

        /// <summary>
        /// Creates the log request.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="messageFactory">The message factory.</param>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        private LogRequest CreateLogRequest(Level level, Func<string> messageFactory, Exception exception)
        {
            return new LogRequest()
            {
                Name = Name,
                Exception = exception,
                Level = level,
                MessageFactory = messageFactory
            };
        }

        /// <summary>
        /// Creates the log request.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        private LogRequest CreateLogRequest(Level level, Exception exception, IFormatProvider provider, string format, params object[] args)
        {
            return new LogRequest()
            {
                Name = Name,
                Level = level,
                Exception = exception,
                MessageFactory = CreateMessageFactory(provider, format, args)
            };
        }

        /// <summary>
        /// Creates the message factory.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="format">The format.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        private static Func<string> CreateMessageFactory(IFormatProvider provider, string format, params object[] args)
        {
            return () => string.Format(provider ?? CultureInfo.CurrentCulture, format, args);
        }
    }
}