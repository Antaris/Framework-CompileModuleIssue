// <copyright company="Fresh Egg Limited" file="CoreModule.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx
{
    using System.Collections.Generic;
    using Microsoft.Framework.DependencyInjection;
    using Fx.Security;

    /// <summary>
    /// Provides core services and permissions for the Framework.
    /// </summary>
    public class CoreModule : ModuleBase
    {
        /// <inheritdoc />
        public override IEnumerable<Permission> GetPermissions()
        {
            yield return CorePermissions.Admin;
            yield return CorePermissions.Public;
            yield return CorePermissions.Owner;
        }

        /// <inheritdoc />
        public override IEnumerable<PermissionConvention> GetPermissionConventions()
        {
            yield return CorePermissionConventions.Admin;
            yield return CorePermissionConventions.Anonymous;
            yield return CorePermissionConventions.Authenticated;
            yield return CorePermissionConventions.Author;
            yield return CorePermissionConventions.Contributor;
            yield return CorePermissionConventions.Editor;
            yield return CorePermissionConventions.Moderator;
        }

        /// <inheritdoc />
        public override IEnumerable<ServiceDescriptor> GetServiceDescriptors()
        {
            // Utilties
            yield return ServiceDescriptor.Singleton<IClock, Clock>();
            yield return ServiceDescriptor.Singleton<IInstanceFactory, InstanceFactory>();
            // Identity services
            yield return ServiceDescriptor.Singleton<IIdentityFactory, IdentityFactory>();
        }
    }
}