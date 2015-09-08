// <copyright company="Fresh Egg Limited" file="IModule.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the required contract for implementing a module provider.
    /// </summary>
    public interface IModuleProvider
    {
        /// <summary>
        /// Gets the available modules.
        /// </summary>
        /// <returns>The set of available modules.</returns>
        IEnumerable<IModule> GetModules();
    }
}