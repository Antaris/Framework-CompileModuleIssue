// <copyright company="Fresh Egg Limited" file="Role.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security
{
    using Fx.Data;

    /// <summary>
    /// Represents a role.
    /// </summary>
    public class Role : EntityBase
    {
        /// <summary>
        /// Gets or sets the conventions used by the role.
        /// </summary>
        public string Conventions { get; set; }

        /// <summary>
        /// Gets or sets the name of the role.
        /// </summary>
        public string Name { get; set; }
    }
}