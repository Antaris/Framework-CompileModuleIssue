// <copyright company="Fresh Egg Limited" file="ParameterModel.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Query.Model
{
    using System;

    /// <summary>
    /// Represents a query parameter.
    /// </summary>
    public sealed class ParameterModel : IEquatable<ParameterModel>, IComparable<ParameterModel>
    {
        private readonly ValueComparer<ParameterModel> Comparer = new ValueComparer<ParameterModel>(
            p => p.Name,
            p => p.Type
        );

        /// <summary>
        /// Initialises a new instance of <see cref="ParameterModel"/>
        /// </summary>
        /// <param name="name">The parameter name.</param>
        /// <param name="type">The parameter type.</param>
        public ParameterModel(string name, Type type)
        {
            Name = Ensure.ArgumentNotNullOrWhiteSpace(name, nameof(name));
            Type = Ensure.ArgumentNotNull(type, nameof(type));
        }

        /// <summary>
        /// Gets the parameter name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the parameter type.
        /// </summary>
        public Type Type { get; private set; }

        /// <inheritdoc />
        public int CompareTo(ParameterModel other)
        {
            return Comparer.Compare(this, other);
        }

        /// <inheritdoc />
        public bool Equals(ParameterModel other)
        {
            return Comparer.Equals(this, other);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return Comparer.Equals(this, obj as ParameterModel);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return Comparer.GetHashCode(this);
        }
    }
}