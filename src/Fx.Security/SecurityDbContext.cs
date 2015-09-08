// <copyright company="Fresh Egg Limited" file="User.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Security
{
    using Fx.Data;
    using Microsoft.Data.Entity;

    /// <summary>
    /// Provides database services for security-related models.
    /// </summary>
    public class SecurityDbContext : DbContextBase
    {
        /// <inheritdoc />
        public DbSet<Role> Roles { get; set; }

        /// <inheritdoc />
        public DbSet<RoleDataSet> RoleDataSets { get; set; }

        /// <inheritdoc />
        public DbSet<RolePermission> RolePermissions { get; set; }

        /// <inheritdoc />
        public DbSet<User> Users { get; set; }

        /// <inheritdoc />
        public DbSet<UserDataSet> UserDataSets { get; set; }

        /// <inheritdoc />
        public DbSet<UserPermission> UserPermissions { get; set; }
    }
}