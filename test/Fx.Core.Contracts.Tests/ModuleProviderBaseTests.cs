// <copyright company="Fresh Egg Limited" file="ModuleProviderBaseTests.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Core.Contracts.Tests
{
    using System.Linq;
    using Xunit;
    using Mocks;

    /// <summary>
    /// Provides tests for the <see cref="ModuleProviderBase"/> type.
    /// </summary>
    public class ModuleProviderBaseTests
    {
        [Fact]
        public void CanAddModuleToModuleProvider()
        {
            // Arrange
            var provider = new TestModuleProvider();

            // Act
            provider.AddModule<TestModule>();

            // Assert
            var modules = provider.GetModules().ToList();
            Assert.True(modules.Count == 1);
            Assert.True(modules[0] is TestModule);
        }

        [Fact]
        public void SupportsMultipleModules()
        {
            // Arrange
            var provider = new TestModuleProvider();

            // Act
            provider.AddModule<TestModule>();
            provider.AddModule<OtherTestModule>();

            // Assert
            var modules = provider.GetModules().ToList();
            Assert.True(modules.Count == 2);
            Assert.True(modules.Any(m => m is TestModule));
            Assert.True(modules.Any(m => m is OtherTestModule));
        }

        [Fact]
        public void ReturnsModulesInOrder()
        {
            // Arrange
            var provider = new TestModuleProvider();

            // Act
            provider.AddModule<TestModule>(); // Order = 0
            provider.AddModule<OtherTestModule>(); // Order = 1

            // Assert
            var modules = provider.GetModules().ToList();
            Assert.True(modules.Count == 2);
            Assert.True(modules[0] is TestModule);
            Assert.True(modules[1] is OtherTestModule);
        }
    }
}