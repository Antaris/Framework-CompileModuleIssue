// <copyright company="Fresh Egg Limited" file="TestModuleProvider.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Core.Contracts.Tests.Mocks
{
    /// <summary>
    /// Provides a test implementation of a module provider.
    /// </summary>
    public class TestModuleProvider : ModuleProviderBase
    {
        /// <summary>
        /// Adds a module to the provider.
        /// </summary>
        /// <typeparam name="T">The module type.</typeparam>
        public new void AddModule<T>() where T : IModule, new()
        {
            base.AddModule<T>();
        }
    }
}