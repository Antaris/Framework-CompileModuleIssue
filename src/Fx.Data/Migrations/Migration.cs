// <copyright company="Fresh Egg Limited" file="Migration.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Provides a base implementation of a migration.
    /// </summary>
    public abstract class Migration : IMigration
    {
        private readonly Lazy<SemanticVersion> _versionThunk;

        /// <summary>
        /// Initialises a new instance of <see cref="Migration"/>
        /// </summary>
        protected Migration()
        {
            _versionThunk = new Lazy<SemanticVersion>(ResolveVersion);
        }

        /// <inheritdoc />
        public SemanticVersion Version {  get { return _versionThunk.Value; } }

        /// <inheritdoc />
        public abstract void Execute();

        /// <summary>
        /// Executes a migration script from an embedded resource.
        /// </summary>
        /// <param name="resourceName">The resource name.</param>
        /// <returns>The task.</returns>
        public Task ExecuteScriptAsync(string resourceName)
        {
            return Task.FromResult(0);
        }

        /// <summary>
        /// Resolves the semantic version for this migration.
        /// </summary>
        /// <returns>The migration version.</returns>
        private SemanticVersion ResolveVersion()
        {
            var type = GetType();
            var attribute = type.GetCustomAttributes(typeof(VersionedMigrationAttribute), false).Cast<VersionedMigrationAttribute>().SingleOrDefault();
            if (attribute == null)
            {
                return new SemanticVersion(0);
            }

            return attribute.Version;
        }
    }
}