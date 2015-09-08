// <copyright company="Fresh Egg Limited" file="DataSetEntityBase.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security
{
    using Fx.Data;

    /// <summary>
    /// Provides a base implementation of a dataset entity.
    /// </summary>
    public abstract class DataSetEntityBase : EntityBase
    {
        /// <summary>
        /// Gets or sets whether the create permission is allowed.
        /// </summary>
        public bool Create { get; set; }

        /// <summary>
        /// Gets or sets the dataset name.
        /// </summary>
        public string DataSetName { get; set; }

        /// <summary>
        /// Gets or sets whether the delete permission is allowed.
        /// </summary>
        public bool Delete { get; set; }

        /// <summary>
        /// Gets or sets whether the read permission is allowed.
        /// </summary>
        public bool Read { get; set; }

        /// <summary>
        /// Gets or sets whether the update permission is allowed.
        /// </summary>
        public bool Update { get; set; }
    }
}