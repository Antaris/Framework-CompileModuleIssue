// <copyright company="Fresh Egg Limited" file="DataSetPermission.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security
{
    using System.Diagnostics;

    /// <summary>
    /// Represents the application of dataset permission.
    /// </summary>
    [DebuggerDisplay("DataSet Permission: {DataSet.Name} (Create = {Create}, Read = {Read}, Update = {Update}, Delete = {Delete})")]
    public class DataSetPermission
    {
        /// <summary>
        /// Initializes a new instance of <see cref="DataSetPermission"/>.
        /// </summary>
        /// <param name="dataset">The dataset.</param>
        /// <param name="create">Flag to state whether this permission is for a create operation.</param>
        /// <param name="read">Flag to state whether this permission is for a read operation.</param>
        /// <param name="update">Flag to state whether this permission is for an update operation.</param>
        /// <param name="delete">Flag to state whether this permission is for a delete operation.</param>
        public DataSetPermission(DataSet dataset, bool create, bool read, bool update, bool delete)
        {
            DataSet = Ensure.ArgumentNotNull(dataset, nameof(dataset));
            Create = create;
            Read = read;
            Update = update;
            Delete = delete;
        }

        /// <summary>
        /// Gets whether this permission is for a create operation.
        /// </summary>
        public bool Create { get; set; }

        /// <summary>
        /// Gets the dataset.
        /// </summary>
        public DataSet DataSet { get; set; }

        /// <summary>
        /// Gets whether this permission is for a delete operation.
        /// </summary>
        public bool Delete { get; set; }

        /// <summary>
        /// Gets whether this permission is for a read operation.
        /// </summary>
        public bool Read { get; set; }

        /// <summary>
        /// Gets whether this permission is for an update operation.
        /// </summary>
        public bool Update { get; set; }
    }
}