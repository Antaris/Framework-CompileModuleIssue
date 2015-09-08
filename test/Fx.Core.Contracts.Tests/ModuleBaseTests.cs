// <copyright company="Fresh Egg Limited" file="ModuleBaseTests.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Core.Contracts.Tests
{
    using System.Linq;
    using System.Reflection;
    using Xunit;
    using Mocks;

    /// <summary>
    /// Provides tests for the <see cref="ModuleBase"/> type.
    /// </summary>
    public class ModuleBaseTests
    {
        [Fact]
        public void ResolvesVersionFromAssemblyByDefault()
        {
            // Arrange
            var assembly = GetType().Assembly;
            var assemblyInformationalVersion = assembly.GetCustomAttributes<AssemblyInformationalVersionAttribute>().Single();
            var assemblyVersion = SemanticVersion.Parse(assemblyInformationalVersion.InformationalVersion);
            var module = new OtherTestModule();

            // Act
            var moduleVersion = module.Version;

            // Assert
            Assert.Equal(assemblyVersion, moduleVersion);
        }

        [Fact]
        public void ResolvesModuleNameFromTypeNameByDefault()
        {
            // Arrange
            var module = new OtherTestModule();

            // Act
            string name = module.Name;

            // Assert
            Assert.Equal("OtherTest", name);
        }

        [Fact]
        public void ResolvesLevel4OrderByDefault()
        {
            // Arrange
            var module = new OtherTestModule();

            // Act
            int order = module.Order;

            // Assert
            Assert.Equal(ModuleOrder.Level4, order);
        }
    }
}