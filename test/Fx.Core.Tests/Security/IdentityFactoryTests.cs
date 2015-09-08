// <copyright company="Fresh Egg Limited" file="IdentityFactoryTests.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security.Tests
{
    using System;
    using Xunit;
    using Fx.Security;


    /// <summary>
    /// Provides tests for the <see cref="IdentityFactory"/> type.
    /// </summary>
    public class IdentityFactoryTests
    {
        [Fact]
        public void CanCreateAnonymousIdentity()
        {
            // Arrange
            var factory = new IdentityFactory(new Clock());

            // Act
            var identity = factory.CreateAnonymousIdentity();

            // Assert
            Assert.True(identity.IsAnonymous);
        }

        [Fact]
        public void CanCreateSystemIdentity()
        {
            // Arrange
            var factory = new IdentityFactory(new Clock());

            // Act
            var identity = factory.CreateSystemIdentity();

            // Assert
            Assert.True(identity.IsSystem);
        }

        [Fact]
        public void CanCreateIdentity()
        {
            // Arrange
            var factory = new IdentityFactory(new Clock());
            var dateTime = DateTimeOffset.UtcNow;

            // Act
            var identity = factory.CreateIdentity(1, "Admin", "admin@freshegg.com", false, false, IdentityStatus.Online, dateTime);

            // Assert
            Assert.Equal(1, identity.Id);
            Assert.Equal("Admin", identity.Name);
            Assert.Equal("admin@freshegg.com", identity.Email);
            Assert.Equal(false, identity.IsAnonymous);
            Assert.Equal(false, identity.IsSystem);
            Assert.Equal(IdentityStatus.Online, identity.Status);
            Assert.Equal(dateTime, identity.DateTimeOffset);
        }

        [Fact]
        public void UsesClockDerivedDateTimeWhenNotProvided()
        {
            // Arrange
            var clock = new Clock();
            var factory = new IdentityFactory(clock);
            var dateTime = DateTimeOffset.UtcNow;
            
            using (clock.Fixed(dateTime))
            {
                // Act
                var identity = factory.CreateIdentity(1, "Admin", "admin@freshegg.com");

                // Assert
                Assert.Equal(dateTime, identity.DateTimeOffset);
            }
        }
    }
}