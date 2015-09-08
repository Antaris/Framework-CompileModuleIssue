// <copyright company="Fresh Egg Limited" file="Clock.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx
{
    using System;

    /// <summary>
    /// Represents a clock to provide date/time (with offset to UTC).
    /// </summary>
    public class Clock : IClock
    {
        private Func<DateTimeOffset> _accessor = () => GetNormalisedDateTimeOffset();

        /// <inheritdoc />
        public DateTimeOffset UtcNow { get { return _accessor(); } }

        /// <summary>
        /// Gets the
        /// </summary>
        /// <returns></returns>
        public static DateTimeOffset GetNormalisedDateTimeOffset()
        {
            var dateTime = DateTimeOffset.UtcNow;

            // Removes the millisecond component from the date/time.
            dateTime.AddTicks(-(dateTime.Ticks % TimeSpan.TicksPerMillisecond));

            return dateTime;
        }

        /// <summary>
        /// Fixes the clock to the given date/time until the adjustment is disposed.
        /// </summary>
        /// <param name="fixedDateTimeOffset">The fixed date/time offset.</param>
        /// <returns>The disposable clock adjustment.</returns>
        public IDisposable Fixed(DateTimeOffset fixedDateTimeOffset)
        {
            return new FixedClockAdjustment(this, fixedDateTimeOffset, _accessor);
        }

        private class FixedClockAdjustment : IDisposable
        {
            private readonly Func<DateTimeOffset> _lastAccessor;
            private readonly Clock _clock;
            private bool _disposed = false;

            /// <summary>
            /// Initialises a new instance of <see cref="FixedClockAdjustment"/>
            /// </summary>
            /// <param name="clock">The clock.</param>
            /// <param name="dateTimeOffset">The date/time (offset to UTC).</param>
            /// <param name="lastAccessor">The last accessor value.</param>
            internal FixedClockAdjustment(Clock clock, DateTimeOffset dateTimeOffset, Func<DateTimeOffset> lastAccessor)
            {
                clock._accessor = () => dateTimeOffset;
                _lastAccessor = lastAccessor;
                _clock = clock;
            }

            /// <inheritdoc />
            public void Dispose()
            {
                if (!_disposed)
                {
                    _clock._accessor = _lastAccessor;
                    _disposed = true;
                }
            }
        }
    }
}