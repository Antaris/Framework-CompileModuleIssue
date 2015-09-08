// <copyright company="Fresh Egg Limited" file="PermissionConvention.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security
{
    using System;
    using System.Diagnostics;
    using System.Linq;

    /// <summary>
    /// Represents a convention for permission application.
    /// </summary>
    [DebuggerDisplay("Permission Convention: {Code} ({PermissionsDebug()})")]
    public class PermissionConvention : IComparable<PermissionConvention>, IEquatable<PermissionConvention>, IComparable
    {
        private static readonly ValueComparer<PermissionConvention> Comparer = new ValueComparer<PermissionConvention>(
            p => p.Code
        );

        /// <summary>
        /// Initialises a new instance of <see cref="PermissionConvention"/>
        /// </summary>
        /// <param name="code">The permission convention code.</param>
        /// <param name="name">The name of the permission convention.</param>
        public PermissionConvention(string code, string name, params Permission[] permissions)
        {
            Ensure.ArgumentNotNullOrWhiteSpace(code, nameof(code));
            Ensure.ArgumentNotNullOrWhiteSpace(name, nameof(name));

            Code = string.Intern(code);
            Name = name;
            Permissions = permissions ?? new Permission[0];
        }

        /// <summary>
        /// Gets the permission code.
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// Gets the name of the permission.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the set of permissions provided by the convention.
        /// </summary>
        public Permission[] Permissions { get; private set; }

        /// <inheritdoc />
        public int CompareTo(PermissionConvention other)
        {
            return Comparer.Compare(this, other);
        }

        /// <inheritdoc />
        int IComparable.CompareTo(object obj)
        {
            return CompareTo(obj as PermissionConvention);
        }

        /// <inheritdoc />
        public bool Equals(PermissionConvention other)
        {
            return Comparer.Equals(this, other);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return Equals(obj as PermissionConvention);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return Comparer.GetHashCode(this);
        }

        private string PermissionsDebug()
        {
            return string.Join(",", Permissions.Select(p => p.Code));
        }
    }
}