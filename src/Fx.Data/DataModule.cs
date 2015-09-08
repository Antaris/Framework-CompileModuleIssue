// <copyright company="Fresh Egg Limited" file="DataModule.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Data
{
    using System.Collections.Generic;
    using Microsoft.Framework.DependencyInjection;

    /// <summary>
    /// Provides core data services.
    /// </summary>
    public class DataModule : ModuleBase
    {
        /// <inheritdoc />
        public override int Order { get { return ModuleOrder.Level0 + 5; } }

        /// <inheritdoc />
        public override IEnumerable<ServiceDescriptor> GetServiceDescriptors()
        {
            // Connection string factory.
            yield return ServiceDescriptor.Singleton<IConnectionStringFactory, ConnectionStringFactory>();
        }
    }
}