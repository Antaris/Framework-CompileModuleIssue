// <copyright company="Fresh Egg Limited" file="InstanceFactoryTests.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Tests
{
    using Xunit;
    using Mocks;

    /// <summary>
    /// Provides tests for the <see cref="InstanceFactory"/> type.
    /// </summary>
    public class InstanceFactoryTests
    {
        [Fact]
        public void CanCreateObjectInstance_WithSingleArgumentConstructor()
        {
            // Arrange
            var factory = new InstanceFactory();

            // Act
            var dog = factory.Create<Dog, string>("Thunder");

            // Assert
            Assert.NotEqual(null, dog);
            Assert.Equal("Thunder", dog.Name);
        }

        [Fact]
        public void CanCreateObjectInstance_WithTwoArgumentConstructor()
        {
            // Arrange
            var factory = new InstanceFactory();

            // Act
            var person = factory.Create<Person, string, string>("Matt", "Abbott");

            // Assert
            Assert.NotEqual(null, person);
            Assert.Equal("Matt", person.Forename);
            Assert.Equal("Abbott", person.Surname);
        }
    }
}
