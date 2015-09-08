// <copyright company="Fresh Egg Limited" file="SecurityDataSets.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security
{
    /// <summary>
    /// Provides access to security dataset descriptors.
    /// </summary>
    public static class SecurityDataSets
    {
        public static readonly DataSet Users = new DataSet<User>("Users", description: "Users DataSet", category: "Security", impliedBy: SecurityPermissions.Users);
        public static readonly DataSet UserPermissions = new DataSet<UserPermission>("UserPermissions", description: "User Permissions DataSet", category: "Security", impliedBy: SecurityPermissions.Users);
        public static readonly DataSet UserDataSets = new DataSet<UserDataSet>("UserDataSets", description: "User DataSets DataSet", category: "Security", impliedBy: SecurityPermissions.Users);

        public static readonly DataSet Roles = new DataSet<Role>("Roles", description: "Roles DataSet", category: "Security", impliedBy: SecurityPermissions.Roles);
        public static readonly DataSet RolePermissions = new DataSet<RolePermission>("RolePermissions", description: "Role Permissions DataSet", category: "Security", impliedBy: SecurityPermissions.Roles);
        public static readonly DataSet RoleDataSets = new DataSet<RoleDataSet>("RoleDataSets", description: "Role DataSets DataSet", category: "Security", impliedBy: SecurityPermissions.Roles);
    }
}