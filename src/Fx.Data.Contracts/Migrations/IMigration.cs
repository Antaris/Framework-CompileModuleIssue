// <copyright company="Fresh Egg Limited" file="IMigration.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Data
{
    /// <summary>
    /// Defines the required contract for implementing a migration.
    /// </summary>
    public interface IMigration
    {
        /// <summary>
        /// Gets the version for this migration.
        /// </summary>
        SemanticVersion Version { get; }

        /// <summary>
        /// Executes the migration.
        /// </summary>
        void Execute();
    }
}