// <copyright company="Fresh Egg Limited" file="ModuleProviderBase.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Provides a base implementation of a module provider.
    /// </summary>
    public abstract class ModuleProviderBase : IModuleProvider
    {
        private readonly List<IModule> _modules = new List<IModule>();

        /// <summary>
        /// Initialises a new instance of <see cref="ModuleProviderBase"/>
        /// </summary>
        protected ModuleProviderBase()
        {
            Setup();
        }

        /// <inheritdoc />
        public IEnumerable<IModule> GetModules()
        {
            return _modules.OrderBy(m => m.Order);
        }


        /// <summary>
        /// Adds a module to
        /// </summary>
        /// <typeparam name="T">The module type.</typeparam>
        protected void AddModule<T>() where T : IModule, new()
        {
            var module = new T();
            _modules.Add(module);
        }

        /// <summary>
        /// Configures the modules instances this provider returns.
        /// </summary>
        protected virtual void Setup() { }
    }
}