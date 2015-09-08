// <copyright company="Fresh Egg Limited" file="FilterOperator.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Query.Model
{
    /// <summary>
    /// Defines the possible operators that can be applied in filters
    /// </summary>
    public enum FilterOperator
    {
        /// <summary>
        /// Compare values using equality
        /// </summary>
        Equal,

        /// <summary>
        /// Compare values using negated equality
        /// </summary>
        NotEqual,

        /// <summary>
        /// Compare values using less-than
        /// </summary>
        LessThan,

        /// <summary>
        /// Compare values using less-than-equal-to
        /// </summary>
        LessThanEqualTo,

        /// <summary>
        /// Compare values using greater-than
        /// </summary>
        GreaterThan,

        /// <summary>
        /// Compare values using greater-than-equal-to
        /// </summary>
        GreaterThanEqualTo,

        /// <summary>
        /// Determine if value lies between two boundary values
        /// </summary>
        Between,

        /// <summary>
        /// Determine if the value lies within the target string
        /// </summary>
        Contains,

        /// <summary>
        /// Determine if the value lies at the start of the target string
        /// </summary>
        StartsWith,

        /// <summary>
        /// Determine if the value lies at the end of the target string
        /// </summary>
        EndsWith,

        /// <summary>
        /// Determine if the value is try
        /// </summary>
        True,

        /// <summary>
        /// Determine if the value is false
        /// </summary>
        False,

        /// <summary>
        /// Determine if a given sub-filter applies to all values in a subset of values
        /// </summary>
        All,

        /// <summary>
        /// Determine if a given sub-filter applies to any values in a subset of values
        /// </summary>
        Any
    }
}
