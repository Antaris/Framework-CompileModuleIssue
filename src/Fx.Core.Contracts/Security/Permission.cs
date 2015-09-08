// <copyright company="Fresh Egg Limited" file="Permission.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security
{
    using System;
    using System.Diagnostics;


    /// <summary>
    /// Represents a permission.
    /// </summary>
    [DebuggerDisplay("Permission: {Code} ({Name})")]
    public class Permission : IComparable<Permission>, IEquatable<Permission>, IComparable
    {
        private static readonly ValueComparer<Permission> Comparer = new ValueComparer<Permission>(
            p => p.Code
        );

        /// <summary>
        /// Initialises a new instance of <see cref="Permission"/>
        /// </summary>
        /// <param name="code">The permission code.</param>
        /// <param name="name">The name of the permission.</param>
        /// <param name="description">[Optional] The permission description.</param>
        /// <param name="category">[Optional] The permission category.</param>
        /// <param name="impliedBy">[Optional] The set of permissions that imply this permission.</param>
        public Permission(string code, string name, string description = null, string category = null, params Permission[] impliedBy)
        {
            Ensure.ArgumentNotNullOrWhiteSpace(code, nameof(code));
            Ensure.ArgumentNotNullOrWhiteSpace(name, nameof(name));

            Code = string.Intern(code);
            Name = name;

            Description = description;
            Category = category;
            ImpliedBy = impliedBy ?? new Permission[0];
        }

        /// <summary>
        /// Gets or sets the permission category.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets the permission code.
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// Gets or sets the permission description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the permissions that imply this permission.
        /// </summary>
        public Permission[] ImpliedBy { get; set; }

        /// <summary>
        /// Gets the name of the permission.
        /// </summary>
        public string Name { get; private set; }

        /// <inheritdoc />
        public int CompareTo(Permission other)
        {
            return Comparer.Compare(this, other);
        }

        /// <inheritdoc />
        int IComparable.CompareTo(object obj)
        {
            return CompareTo(obj as Permission);
        }

        /// <inheritdoc />
        public bool Equals(Permission other)
        {
            return Comparer.Equals(this, other);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return Equals(obj as Permission);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return Comparer.GetHashCode(this);
        }
    }
}