// <copyright company="Fresh Egg Limited" file="CorePermissions.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx
{
    using Fx.Security;

    /// <summary>
    /// Provides access to core permission descriptors.
    /// </summary>
    public static class CorePermissions
    {
        public static readonly Permission Admin = new Permission("Admin", "Administration Access", description: "Allows access to the administration area.", category: "Area");
        public static readonly Permission Public = new Permission("Public", "Public Access", description: "Allows access to the public front-end of the website.", category: "Area");
        public static readonly Permission Owner = new Permission("Owner", "Site Owner");
    }
}