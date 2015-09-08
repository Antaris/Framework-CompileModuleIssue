// <copyright company="Fresh Egg Limited" file="Reader.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Data
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Data.Entity;
    using Fx.Security;

    /// <summary>
    /// Provides read services for entities.
    /// </summary>
    /// <typeparam name="C">The Db context type.</typeparam>
    /// <typeparam name="E">The entity type.</typeparam>
    public class Reader<C, E> : IReader<E>
        where C : DbContextBase
        where E : EntityBase
    {
        private readonly C _context;
        private readonly IWorkContext _workContext;
        private readonly DbSet<E> _set;
        private readonly DataSet _dataset;

        /// <summary>
        /// Initialises a new instance of <see cref="Reader{C,E}"/>.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="workContext">The work context.</param>
        /// <param name="dataset">[Optional] The dataset that manages security for data provided by this context.</param>
        public Reader(C context, IWorkContext workContext, DataSet dataset = null)
        {
            _context = Ensure.ArgumentNotNull(context, nameof(context));
            _workContext = Ensure.ArgumentNotNull(workContext, nameof(workContext));
            _set = context.Set<E>();
            _dataset = dataset;
        }

        /// <summary>
        /// Gets the database context.
        /// </summary>
        public C Context { get { return _context; } }

        /// <summary>
        /// Gets the entity set.
        /// </summary>
        public virtual DbSet<E> Set {  get { return _set; } }

        /// <summary>
        /// Gets the entity set as a queryable interface.
        /// </summary>
        public virtual IQueryable<E> QuerySet {  get { return _set.Where(e => !e.Deleted).AsQueryable(); } }

        /// <summary>
        /// Gets the work context.
        /// </summary>
        public IWorkContext WorkContext {  get { return _workContext; } }

        /// <inheritdoc />
        public virtual IQueryable<E> Command(string command, params object[] arguments)
        {
            return Set.FromSql(command, arguments);
        }

        /// <inheritdoc />
        public virtual bool Exists(int id)
        {
            return Exists(e => e.Id == id);
        }

        /// <inheritdoc />
        public virtual bool Exists(Expression<Func<E, bool>> predicate)
        {
            return ExistsAsync(predicate).Result;
        }

        /// <inheritdoc />
        public virtual Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return ExistsAsync(e => e.Id == id, cancellationToken);
        }

        /// <inheritdoc />
        public virtual async Task<bool> ExistsAsync(Expression<Func<E, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
        {
            ValidateDataSetPermission(DataSetPermissionType.Read);

            return await QuerySet.AnyAsync(predicate, cancellationToken);
        }

        /// <inheritdoc />
        public virtual E Get(int id, Func<IQueryable<E>, IQueryable<E>> query = null)
        {
            return GetSingle(e => e.Id == id, query);
        }

        /// <inheritdoc />
        public virtual async Task<E> GetAsync(int id, Func<IQueryable<E>, IQueryable<E>> query = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetSingleAsync(e => e.Id == id, query, cancellationToken);
        }

        /// <inheritdoc />
        public virtual E Get(Expression<Func<E, bool>> predicate, Func<IQueryable<E>, IQueryable<E>> query = null)
        {
            ValidateDataSetPermission(DataSetPermissionType.Read);

            Ensure.ArgumentNotNull(predicate, nameof(predicate));
            query = query ?? ((q) => q);

            return query(QuerySet).Where(predicate).FirstOrDefault();
        }

        /// <inheritdoc />
        public virtual async Task<E> GetAsync(Expression<Func<E, bool>> predicate, Func<IQueryable<E>, IQueryable<E>> query = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            ValidateDataSetPermission(DataSetPermissionType.Read);

            Ensure.ArgumentNotNull(predicate, nameof(predicate));
            query = query ?? ((q) => q);

            return await query(QuerySet).Where(predicate).FirstOrDefaultAsync(cancellationToken);
        }

        /// <inheritdoc />
        public virtual E GetSingle(Expression<Func<E, bool>> predicate, Func<IQueryable<E>, IQueryable<E>> query = null)
        {
            ValidateDataSetPermission(DataSetPermissionType.Read);

            Ensure.ArgumentNotNull(predicate, nameof(predicate));
            query = query ?? ((q) => q);

            return query(QuerySet).Where(predicate).SingleOrDefault();
        }

        /// <inheritdoc />
        public virtual async Task<E> GetSingleAsync(Expression<Func<E, bool>> predicate, Func<IQueryable<E>, IQueryable<E>> query = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            ValidateDataSetPermission(DataSetPermissionType.Read);

            Ensure.ArgumentNotNull(predicate, nameof(predicate));
            query = query ?? ((q) => q);

            return await query(QuerySet).Where(predicate).SingleOrDefaultAsync(cancellationToken);
        }

        /// <inheritdoc />
        public virtual IQueryable<E> GetAll(Expression<Func<E, bool>> predicate = null, Func<IQueryable<E>, IQueryable<E>> query = null)
        {
            ValidateDataSetPermission(DataSetPermissionType.Read);

            query = query ?? ((q) => q);

            var result = query(QuerySet);

            if (predicate != null)
            {
                result = result.Where(predicate);
            }

            return result;
        }

        /// <summary>
        /// Validates that the current security context has the required dataset permission.
        /// </summary>
        /// <param name="type">The dataset permission type.</param>
        protected void ValidateDataSetPermission(DataSetPermissionType type)
        {
            if (_dataset == null)
            {
                // No managing dataset, so don't perform any security checking.
                return;
            }

            if (!WorkContext.SecurityContext.HasDataSetPermission(_dataset, type))
            {
                // The user doesn't have access to this dataset.
                throw new PermissionException(WorkContext.Identity, _dataset, type);
            }
        }
    }
}