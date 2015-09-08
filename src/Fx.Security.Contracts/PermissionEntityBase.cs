// <copyright company="Fresh Egg Limited" file="PermissionEntityBase.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security
{
    using Fx.Data;

    /// <summary>
    /// Provides a base implementation of a permission entity.
    /// </summary>
    public abstract class PermissionEntityBase : EntityBase
    {
        /// <summary>
        /// Gets or sets the permission code.
        /// </summary>
        public string PermissionCode { get; set; }

        /// <summary>
        /// Gets or sets whether the permission is denied.
        /// </summary>
        public bool Deny { get; set; }
    }
}