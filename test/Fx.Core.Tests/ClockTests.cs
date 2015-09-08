// <copyright company="Fresh Egg Limited" file="ClockTests.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Tests
{
    using System;
    using Xunit;

    /// <summary>
    /// Provides tests for the <see cref="Clock"/> type.
    /// </summary>
    public class ClockTests
    {
        [Fact]
        public void CanTemporarilyFixClockResult()
        {
            // Arrange
            var clock = new Clock();
            var dateTime = DateTimeOffset.Now;
            
            using (clock.Fixed(dateTime))
            {
                // Act
                var result = clock.UtcNow;

                // Assert
                Assert.Equal(dateTime, result);
            }
        }

        [Fact]
        public void CanRevertFixedClockResult()
        {
            // Arrange
            var clock = new Clock();
            var dateTime = DateTimeOffset.Now.AddDays(-1);
            var disposable = clock.Fixed(dateTime);

            // Act
            disposable.Dispose();
            var result = clock.UtcNow;

            // Assert
            Assert.NotEqual(dateTime, result);
        }
    }
}