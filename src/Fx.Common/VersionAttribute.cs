// <copyright company="Fresh Egg Limited" file="VersionAttribute.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx
{
    using System;

    /// <summary>
    /// Marks an assembly with a specific semantic version string.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public sealed class VersionAttribute : Attribute
    {
        /// <summary>
        /// Intialises a new instance of <see cref="VersionAttribute"/>
        /// </summary>
        /// <param name="semanticVersion">The semantic version.</param>
        public VersionAttribute(string semanticVersion)
        {
            Ensure.ArgumentNotNullOrWhiteSpace(semanticVersion, nameof(semanticVersion));

            Version = SemanticVersion.Parse(semanticVersion);
        }

        /// <summary>
        /// Gets the semantic version represented by this attribute.
        /// </summary>
        public SemanticVersion Version { get; private set; }
    }
}