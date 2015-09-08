// <copyright company="Fresh Egg Limited" file="FilterLogic.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Query.Model
{
    /// <summary>
    /// Defines the binary combination logic for filter groups.
    /// </summary>
    public enum FilterGroupLogic
    {
        /// <summary>
        /// Combine filters using binary AND logic
        /// </summary>
        And,

        /// <summary>
        /// Combine filters using binary OR logic
        /// </summary>
        Or,

        /// <summary>
        /// Combine filters using binary XOR logic
        /// </summary>
        Xor
    }
}