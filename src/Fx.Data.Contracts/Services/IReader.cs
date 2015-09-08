// <copyright company="Fresh Egg Limited" file="IReader.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Data
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the required contract for implementing a reader.
    /// </summary>
    public interface IReader<T> where T : EntityBase
    {
        /// <summary>
        /// Performs a custom command on the store.
        /// </summary>
        /// <param name="command">The command text</param>
        /// <param name="parameters">The command parameters</param>
        /// <returns>The command result set.</returns>
        IQueryable<T> Command(string command, params object[] parameters);

        /// <summary>
        /// Determines if an entity exists with the given id.
        /// </summary>
        /// <param name="id">The entity id.</param>
        /// <returns>True if the entity exists, otherwise false.</returns>
        bool Exists(int id);

        /// <summary>
        /// Determines if an entity exists that matches the given predicate.
        /// </summary>
        /// <param name="predicate">The predicate to match</param>
        /// <returns>True if an entity exists, otherwise false.</returns>
        bool Exists(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Determines if an entity exists that matches the given id asynchronously
        /// </summary>
        /// <param name="id">The id of the entity</param>
        /// <param name="cancellationToken">[Optional] The cancellation token.</param>
        /// <returns>The task instance.</returns>
        Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Determines if an entity exists that matches the given predicate asynchronously
        /// </summary>
        /// <param name="predicate">The predicate to match</param>
        /// <param name="cancellationToken">[Optional] The cancellation token.</param>
        /// <returns>The task instance.</returns>
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the entity with the specified id.
        /// </summary>
        /// <param name="id">The id of the entity.</param>
        /// <param name="query">[Optional] The query updater.</param>
        /// <returns>The entity instance.</returns>
        T Get(int id, Func<IQueryable<T>, IQueryable<T>> query = null);

        /// <summary>
        /// Gets the entity with the specified id asynchronously.
        /// </summary>
        /// <param name="id">The id of the entity.</param>
        /// <param name="query">[Optional] The query updater.</param>
        /// <param name="cancellationToken">[Optional] The cancellation token.</param>
        /// <returns>The task instance.</returns>
        Task<T> GetAsync(int id, Func<IQueryable<T>, IQueryable<T>> query = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets first the entity that matches the given predicate.
        /// </summary>
        /// <param name="predicate">The predicate to match.</param>
        /// <param name="query">[Optional] The query updater.</param>
        /// <returns>The entity instance.</returns>
        T Get(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>> query = null);

        /// <summary>
        /// Gets first the entity that matches the given predicate asynchronously.
        /// </summary>
        /// <param name="predicate">The predicate to match.</param>
        /// <param name="query">[Optional] The query updater.</param>
        /// <param name="cancellationToken">[Optional] The cancellation token.</param>
        /// <returns>The task instance.</returns>
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>> query = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the single entity that matches the given predicate.
        /// </summary>
        /// <param name="predicate">The predicate to match.</param>
        /// <param name="query">[Optional] The query updater.</param>
        /// <returns>The entity instance.</returns>
        T GetSingle(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>> query = null);

        /// <summary>
        /// Gets the single entity that matches the given predicate asynchronously.
        /// </summary>
        /// <param name="predicate">The predicate to match.</param>
        /// <param name="query">[Optional] The query updater.</param>
        /// <param name="cancellationToken">[Optional] The cancellation token.</param>
        /// <returns>The task instance.</returns>
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IQueryable<T>> query = null, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets all the entities of the given type, optionally matching the predicate provided.
        /// </summary>
        /// <param name="predicate">[Optional] The predicate to match.</param>
        /// <param name="query">[Optional] The query updater.</param>
        /// <returns>The set of matching entities.</returns>
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IQueryable<T>> query = null);
    }
}