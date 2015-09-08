// <copyright company="Fresh Egg Limited" file="PropertyModel.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Query.Model
{
    using System;

    /// <summary>
    /// Represents a property in a query.
    /// </summary>
    public sealed class PropertyModel : IEquatable<PropertyModel>, IComparable<PropertyModel>
    {
        private readonly ValueComparer<PropertyModel> Comparer = new ValueComparer<PropertyModel>(
            p => p.Path,
            p => p.Type
        );

        /// <summary>
        /// Initialises a new instance of <see cref="PropertyModel"/>
        /// </summary>
        /// <param name="path">The property path.</param>
        /// <param name="type">The property type.</param>
        public PropertyModel(string path, Type type)
        {
            Path = Ensure.ArgumentNotNullOrWhiteSpace(path, nameof(path));
            Type = Ensure.ArgumentNotNull(type, nameof(type));
        }

        /// <summary>
        /// Gets the property path.
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// Gets the property type.
        /// </summary>
        public Type Type { get; private set; }

        /// <inheritdoc />
        public int CompareTo(PropertyModel other)
        {
            return Comparer.Compare(this, other);
        }

        /// <inheritdoc />
        public bool Equals(PropertyModel other)
        {
            return Comparer.Equals(this, other);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return Comparer.Equals(this, obj as PropertyModel);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return Comparer.GetHashCode(this);
        }
    }
}