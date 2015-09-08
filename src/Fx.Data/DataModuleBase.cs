// <copyright company="Fresh Egg Limited" file="DataModuleBase.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Data
{
    using System;
    using Microsoft.Data.Entity;
    using Microsoft.Data.Entity.Infrastructure;


    /// <summary>
    /// Provides a base implementation of a data-aware module.
    /// </summary>
    public abstract class DataModuleBase<T> : ModuleBase, IDataModule where T : DbContextBase
    {
        /// <inheritdoc />
        public void ConfigureDbContext(EntityFrameworkServicesBuilder builder, Action<DbContextOptionsBuilder> optionsAction = null)
        {
            // Register the DB context using the standard service builder.
            builder.AddDbContext<T>(optionsAction);
        }
    }
}