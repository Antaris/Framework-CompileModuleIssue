// <copyright company="Fresh Egg Limited" file="RolePermission.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security
{
    /// <summary>
    /// Represents a role permission.
    /// </summary>
    public class RolePermission : PermissionEntityBase
    {
        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        public virtual Role Role { get; set; }

        /// <summary>
        /// Gets or sets the role id.
        /// </summary>
        public int RoleId { get; set; }
    }
}