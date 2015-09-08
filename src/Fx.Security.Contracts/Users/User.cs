// <copyright company="Fresh Egg Limited" file="User.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security
{
    using System.Collections.Generic;
    using Fx.Data;

    /// <summary>
    /// Represents a user.
    /// </summary>
    public class User : EntityBase
    {
        /// <summary>
        /// Gets or sets the conventions used by the user.
        /// </summary>
        public string Conventions { get; set; }

        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the forename of the user.
        /// </summary>
        public string Forename { get; set; }

        /// <summary>
        /// Gets or sets the password for the user.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the collection of roles to which the user is assigned.
        /// </summary>
        public virtual ICollection<Role> Roles { get; set; }

        /// <summary>
        /// Gets or sets the surname of the suer.
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public string Username { get; set; }
    }
}