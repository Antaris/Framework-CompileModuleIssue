// <copyright company="Fresh Egg Limited" file="IDataModule.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Data
{
    using System;
    using Microsoft.Data.Entity;
    using Microsoft.Data.Entity.Infrastructure;

    /// <summary>
    /// Defines the required contract for implementing a data module.
    /// </summary>
    public interface IDataModule
    {
        /// <summary>
        /// Configures the DB context with the dependency system.
        /// </summary>
        /// <param name="builder">The entity framework services builder.</param>
        /// <param name="optionAction">[Optional] The action used to configure per context options.</param>
        void ConfigureDbContext(EntityFrameworkServicesBuilder builder, Action<DbContextOptionsBuilder> optionAction = null);
    }
}