// <copyright company="Fresh Egg Limited" file="FilterModel.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Query.Model
{
    using System;

    /// <summary>
    /// Represents a query filter
    /// </summary>
    public class FilterModel : IEquatable<FilterModel>, IComparable<FilterModel>
    {
        private readonly int _cachedHashCode;

        private readonly ValueComparer<FilterModel> Comparer = new ValueComparer<FilterModel>(
            f => f.Constant,
            f => f.Operator,
            f => f.Parameter,
            f => f.Property
        );

        /// <summary>
        /// Initialises a new instance of <see cref="FilterModel"/>
        /// </summary>
        /// <param name="property">The property</param>
        /// <param name="op">The operator</param>
        /// <param name="constant">The constant value.</param>
        public FilterModel(PropertyModel property, FilterOperator op, object constant)
        {
            Property = Ensure.ArgumentNotNull(property, nameof(property));
            Operator = op;
            Constant = constant;

            _cachedHashCode = Comparer.GetHashCode(this);
        }

        /// <summary>
        /// Initialises a new instance of <see cref="FilterModel"/>
        /// </summary>
        /// <param name="property">The property.</param>
        /// <param name="op">The operator</param>
        /// <param name="parameter">The parameter</param>
        public FilterModel(PropertyModel property, FilterOperator op, ParameterModel parameter)
        {
            Property = Ensure.ArgumentNotNull(property, nameof(property));
            Operator = op;
            Parameter = Ensure.ArgumentNotNull(parameter, nameof(parameter));

            _cachedHashCode = Comparer.GetHashCode(this);
        }

        /// <summary>
        /// Initialises a new instance of <see cref="FilterModel"/>
        /// </summary>
        protected FilterModel()
        {
            _cachedHashCode = Comparer.GetHashCode(this);
        }

        /// <summary>
        /// Gets the constant value for the filter.
        /// </summary>
        public object Constant { get; private set; }

        /// <summary>
        /// Gets the operator for the filter.
        /// </summary>
        public FilterOperator Operator { get; private set; }

        /// <summary>
        /// Gets the target parameter for the filter.
        /// </summary>
        public ParameterModel Parameter { get; private set; }

        /// <summary>
        /// Gets the target property for the filter.
        /// </summary>
        public PropertyModel Property { get; private set; }

        /// <inheritdoc />
        public virtual int CompareTo(FilterModel other)
        {
            return Comparer.Compare(this, other);
        }

        /// <inheritdoc />
        public virtual bool Equals(FilterModel other)
        {
            return Comparer.Equals(this, other);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return Comparer.Equals(this, obj as FilterModel);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return _cachedHashCode;
        }
    }
}