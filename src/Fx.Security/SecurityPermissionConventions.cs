// <copyright company="Fresh Egg Limited" file="SecurityPermissionConventions.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security
{
    /// <summary>
    /// Provides access to security permission convention descriptors.
    /// </summary>
    public static class SecurityPermissionConventions
    {
        public static readonly PermissionConvention Admin = new PermissionConvention("admin", "Administrator", SecurityPermissions.Users, SecurityPermissions.Roles);
    }
}