// <copyright company="Fresh Egg Limited" file="VersionedMigrationAttribute.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Data
{
    using System;

    /// <summary>
    /// Decorates a migration with a version.
    /// </summary>
    public class VersionedMigrationAttribute : Attribute
    {
        /// <summary>
        /// Initialises a new instance of <see cref="VersionedMigrationAttribute"/>
        /// </summary>
        /// <param name="version">The version.</param>
        public VersionedMigrationAttribute(string version)
        {
            Version = SemanticVersion.Parse(version);
        }

        /// <summary>
        /// Gets the version.
        /// </summary>
        public SemanticVersion Version { get; private set; }
    }
}