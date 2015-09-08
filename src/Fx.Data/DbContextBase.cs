// <copyright company="Fresh Egg Limited" file="DbContextBase.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Data
{
    using System;
    using Microsoft.Data.Entity;
    using Microsoft.Data.Entity.Infrastructure;

    /// <summary>
    /// Provides a base implementation of a <see cref="DbContext"/>
    /// </summary>
    public abstract class DbContextBase : DbContext
    {
        public DbContextBase() : base() { }

        public DbContextBase(DbContextOptions options) : base(options) { }

        public DbContextBase(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public DbContextBase(IServiceProvider serviceProvider, DbContextOptions options) : base(serviceProvider, options) { }
    }
}