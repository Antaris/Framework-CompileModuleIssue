// <copyright company="Fresh Egg Limited" file="DefaultLoggingModule.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Logging
{
    using System.Collections.Generic;
    using Microsoft.Framework.DependencyInjection;

    /// <summary>
    /// Registers the default logging services.
    /// </summary>
    public class DefaultLoggingModule : ModuleBase
    {
        /// <inheritdoc />
        public override int Order
        {
            get { return ModuleOrder.Level2; }
        }

        /// <inheritdoc />
        public override IEnumerable<ServiceDescriptor> GetServiceDescriptors()
        {
            yield return ServiceDescriptor.Instance<ILoggerFactory>(new DefaultLoggerFactory());
        }
    }
}