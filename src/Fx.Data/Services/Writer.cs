// <copyright company="Fresh Egg Limited" file="Writer.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Data
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Fx.Security;

    /// <summary>
    /// Provides write services for entities.
    /// </summary>
    /// <typeparam name="C">The Db context type.</typeparam>
    /// <typeparam name="E">The entity type.</typeparam>
    public class Writer<C, E> : Reader<C, E>, IWriter<E>
        where C : DbContextBase
        where E : EntityBase
    {
        private readonly IClock _clock;

        /// <summary>
        /// Initialises a new instance of <see cref="Writer{C,E}"/>.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="workContext">The work context.</param>
        /// <param name="clock">The system clock.</param>
        /// <param name="dataset">[Optional] The dataset that manages security for data provided by this context.</param>
        public Writer(C context, IWorkContext workContext, IClock clock, DataSet dataset = null)
            : base(context, workContext, dataset)
        {
            _clock = Ensure.ArgumentNotNull(clock, nameof(clock));
        }

        /// <inheritdoc />
        public int Create(E entity)
        {
            return CreateAsync(entity).Result;
        }

        /// <inheritdoc />
        public async Task<int> CreateAsync(E entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            var identity = WorkContext.Identity;
            if (identity.IsAnonymous)
            {
                throw new InvalidOperationException("Anonymous identities aren't allowed to save changes.");
            }
            
            ValidateDataSetPermission(DataSetPermissionType.Create);

            entity.Deleted = false;
            entity.CreatedDateTimeOffset = _clock.UtcNow;
            entity.CreatedUserId = identity.Id;

            Set.Add(entity);
            await Context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }

        /// <inheritdoc />
        public async void Delete(int id)
        {
            await DeleteAsync(id);
        }

        /// <inheritdoc />
        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var identity = WorkContext.Identity;
            if (identity.IsAnonymous)
            {
                throw new InvalidOperationException("Anonymous identities aren't allowed to save changes.");
            }

            ValidateDataSetPermission(DataSetPermissionType.Delete);

            var entity = await GetAsync(id, cancellationToken: cancellationToken);
            if (entity != null)
            {
                entity.Deleted = true;
                entity.UpdatedDateTimeOffset = _clock.UtcNow;
                entity.UpdatedUserId = identity.Id;
            }
        }

        /// <inheritdoc />
        public async void Update(E entity)
        {
            await UpdateAsync(entity);
        }

        /// <inheritdoc />
        public async Task UpdateAsync(E entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            var identity = WorkContext.Identity;
            if (identity.IsAnonymous)
            {
                throw new InvalidOperationException("Anonymous identities aren't allowed to save changes.");
            }

            ValidateDataSetPermission(DataSetPermissionType.Update);

            entity.Deleted = false;
            entity.UpdatedDateTimeOffset = _clock.UtcNow;
            entity.UpdatedUserId = identity.Id;

            Context.Entry(entity).State = Microsoft.Data.Entity.EntityState.Modified;
            await Context.SaveChangesAsync(cancellationToken);
        }
    }
}