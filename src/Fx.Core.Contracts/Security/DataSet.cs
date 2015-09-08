// <copyright company="Fresh Egg Limited" file="DataSet.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security
{
    using System;
    using System.Diagnostics;


    /// <summary>
    /// Represents a data set.
    /// </summary>
    [DebuggerDisplay("DataSet: {Name}")]
    public class DataSet : IComparable<DataSet>, IEquatable<DataSet>, IComparable
    {
        private static readonly ValueComparer<DataSet> Comparer = new ValueComparer<DataSet>(
            d => d.Name    
        );

        /// <summary>
        /// Initialises a new instance of <see cref="DataSet"/>
        /// </summary>
        /// <param name="name">The data set name.</param>
        /// <param name="description">[Optional] The dataset description.</param>
        /// <param name="category">[Optional] The dataset category.</param>
        /// <param name="impliedBy">[Optional] The set of permissions that imply this dataset.</param>
        public DataSet(string name, string description = null, string category = null, params Permission[] impliedBy)
        {
            Ensure.ArgumentNotNullOrWhiteSpace(name, nameof(name));

            Name = name;
            Description = description;
            Category = category;
            ImpliedBy = impliedBy ?? new Permission[0];
        }

        /// <summary>
        /// Gets or sets the permission category.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the permission description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the permissions that imply this permission.
        /// </summary>
        public Permission[] ImpliedBy { get; set; }

        /// <summary>
        /// Gets the name of the permission.
        /// </summary>
        public string Name { get; private set; }

        /// <inheritdoc />
        public int CompareTo(DataSet other)
        {
            return Comparer.Compare(this, other);
        }

        /// <inheritdoc />
        int IComparable.CompareTo(object obj)
        {
            return CompareTo(obj as DataSet);
        }

        /// <inheritdoc />
        public bool Equals(DataSet other)
        {
            return Comparer.Equals(this, other);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return Equals(obj as DataSet);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return Comparer.GetHashCode(this);
        }
    }

    /// <summary>
    /// Represents a bound dataset.
    /// </summary>
    /// <typeparam name="T">The bound entity type.</typeparam>
    [DebuggerDisplay("DataSet: {Name} ({Type.FullName})")]
    public class DataSet<T> : DataSet
    {
        /// <summary>
        /// Initialises a new instance of <see cref="DataSet{T}"/>
        /// </summary>
        /// <param name="name">The data set name.</param>
        /// <param name="description">[Optional] The dataset description.</param>
        /// <param name="category">[Optional] The dataset category.</param>
        /// <param name="impliedBy">[Optional] The set of permissions that imply this dataset.</param>
        public DataSet(string name, string description = null, string category = null, params Permission[] impliedBy)
            : base(name, description, category, impliedBy)
        {  }

        /// <summary>
        /// Gets the bound entity type.
        /// </summary>
        public Type Type { get; private set; } = typeof(T);
    }
}
