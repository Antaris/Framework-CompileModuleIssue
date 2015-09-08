// <copyright company="Fresh Egg Limited" file="IWriter.cs">
// Copyright © Fresh Egg Limited
// </copyright>

namespace Fx.Data
{
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the required contract for implementing a writer.
    /// </summary>
    public interface IWriter<T> : IReader<T> where T : EntityBase
    {
        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity to create.</param>
        /// <returns>The id of the new entity.</returns>
        int Create(T entity);

        /// <summary>
        /// Creates the specified entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to create.</param>
        /// <param name="cancellationToken">[Optional] The cancellation token.</param>
        /// <returns>The id of the new entity.</returns>
        Task<int> CreateAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Deletes the entity with the given id.
        /// </summary>
        /// <param name="id">The id of the entity to delete.</param>
        void Delete(int id);

        /// <summary>
        /// Deletes the entity with the given id asynchronously.
        /// </summary>
        /// <param name="id">The id of the entity to delete.</param>
        /// <param name="cancellationToken">[Optional] The cancellation token.</param>
        Task DeleteAsync(int id, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        void Update(T entity);

        /// <summary>
        /// Updates the specified entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <param name="cancellationToken">[Optional] The cancellation token.</param>
        Task UpdateAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));
    }
}