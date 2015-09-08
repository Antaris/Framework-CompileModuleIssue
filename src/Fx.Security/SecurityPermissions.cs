// <copyright company="Fresh Egg Limited" file="SecurityPermissions.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security
{
    /// <summary>
    /// Provides access to security permission descriptors.
    /// </summary>
    public static class SecurityPermissions
    {
        public static readonly Permission Users = new Permission("Users", "User Management", description: "Allows access to user management.", category: "Security");
        public static readonly Permission Roles = new Permission("Roles", "Role Management", description: "Allows access to role management", category: "Security");
    }
}
