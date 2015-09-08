// <copyright company="Fresh Egg Limited" file="UserDataSet.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security
{
    /// <summary>
    /// Represents role permissions for a dataset.
    /// </summary>
    public class RoleDataSet : DataSetEntityBase
    {
        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        public virtual Role Role { get; set; }

        /// <summary>
        /// Gets or sets the role id.
        /// </summary>
        public int RoleId { get; set; }
    }
}