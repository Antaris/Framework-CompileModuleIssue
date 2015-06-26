// <copyright company="Fresh Egg Limited" file="CorePermissionConventions.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx
{
    using Fx.Security;

    /// <summary>
    /// Provides access to core permission convention descriptors.
    /// </summary>
    public static class CorePermissionConventions
    {
        public static readonly PermissionConvention Admin = new PermissionConvention("admin", "Administrator", CorePermissions.Admin, CorePermissions.Public, CorePermissions.Owner);
        public static readonly PermissionConvention Anonymous = new PermissionConvention("anonymous", "Anonymous", CorePermissions.Public);
        public static readonly PermissionConvention Authenticated = new PermissionConvention("authenticated", "Authenticated", CorePermissions.Public);
        public static readonly PermissionConvention Editor = new PermissionConvention("editor", "Editor", CorePermissions.Admin);
        public static readonly PermissionConvention Moderator = new PermissionConvention("moderator", "Moderator", CorePermissions.Admin);
        public static readonly PermissionConvention Author = new PermissionConvention("author", "Author", CorePermissions.Admin);
        public static readonly PermissionConvention Contributor = new PermissionConvention("contributor", "Contributor", CorePermissions.Admin);
    }
}