// <copyright company="Fresh Egg Limited" file="UserPermission.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security
{
    /// <summary>
    /// Represents a user permission.
    /// </summary>
    public class UserPermission : PermissionEntityBase
    {
        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public int UserId { get; set; }
    }
}