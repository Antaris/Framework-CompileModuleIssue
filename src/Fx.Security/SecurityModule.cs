// <copyright company="Fresh Egg Limited" file="SecurityModule.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security
{
    using System.Collections.Generic;
    using Microsoft.Framework.DependencyInjection;
    using Fx.Data;
    
    /// <summary>
    /// Represents the security module.
    /// </summary>
    public class SecurityModule : DataModuleBase<SecurityDbContext>
    {
        /// <inheritdoc />
        public override int Order { get { return ModuleOrder.Level0; } }

        /// <inheritdoc />
        public override IEnumerable<DataSet> GetDataSets()
        {
            yield return SecurityDataSets.Users;
            yield return SecurityDataSets.Roles;
        }

        /// <inheritdoc />
        public override IEnumerable<Permission> GetPermissions()
        {
            yield return SecurityPermissions.Users;
            yield return SecurityPermissions.Roles;
        }

        /// <inheritdoc />
        public override IEnumerable<PermissionConvention> GetPermissionConventions()
        {
            yield return SecurityPermissionConventions.Admin;
        }

        /// <inheritdoc />
        public override IEnumerable<ServiceDescriptor> GetServiceDescriptors()
        {
            // Role services
            yield return ServiceDescriptor.Transient<IRoleService, RoleService>();
            yield return ServiceDescriptor.Transient<IRolePermissionService, RolePermissionService>();
            yield return ServiceDescriptor.Transient<IRoleDataSetService, RoleDataSetService>();
            // User services.
            yield return ServiceDescriptor.Transient<IUserService, UserService>();
            yield return ServiceDescriptor.Transient<IUserPermissionService, UserPermissionService>();
            yield return ServiceDescriptor.Transient<IUserDataSetService, UserDataSetService>();
        }
    }
}