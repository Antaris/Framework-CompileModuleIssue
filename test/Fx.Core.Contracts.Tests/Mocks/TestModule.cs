// <copyright company="Fresh Egg Limited" file="TestModule.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Core.Contracts.Tests.Mocks
{
    using System.Collections.Generic;
    using Fx.Security;
    using Microsoft.Framework.DependencyInjection;

    /// <summary>
    /// Provides a test implementation of a module.
    /// </summary>
    public class TestModule : IModule
    {
        /// <inheritdoc />
        public string Name { get { return "Test"; } }

        /// <inheritdoc />
        public int Order { get { return 0; } }

        /// <inheritdoc />
        public SemanticVersion Version { get { return new SemanticVersion(0); } }

        /// <inheritdoc />
        public IEnumerable<DataSet> GetDataSets()
        {
            yield break;
        }

        /// <inheritdoc />
        public IEnumerable<PermissionConvention> GetPermissionConventions()
        {
            yield break;
        }

        /// <inheritdoc />
        public IEnumerable<Permission> GetPermissions()
        {
            yield break;
        }

        /// <inheritdoc />
        public IEnumerable<ServiceDescriptor> GetServiceDescriptors()
        {
            yield break;
        }
    }
}