// <copyright company="Fresh Egg Limited" file="UserDataSet.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security
{
    /// <summary>
    /// Represents user permissions for a dataset.
    /// </summary>
    public class UserDataSet : DataSetEntityBase
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