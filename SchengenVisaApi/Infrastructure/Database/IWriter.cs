using Domains;

namespace Infrastructure.Database
{
    /// Writes data to data storage
    /// <remarks><see cref="IUnitOfWork"/> should be used to save changes</remarks>
    public interface IWriter
    {
        /// Add <paramref name="entity"/> to data storage
        /// <param name="entity">Entity to add</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <typeparam name="T">Entity type</typeparam>
        Task AddAsync<T>(T entity, CancellationToken cancellationToken) where T : class, IEntity;

        /// Update <paramref name="entity"/> in data storage
        /// <param name="entity">Entity to update</param>
        /// <typeparam name="T">Entity type</typeparam>
        void Update<T>(T entity) where T : class, IEntity;

        /// Remove <paramref name="entity"/> from data storage
        /// <param name="entity">Entity to remove</param>
        /// <typeparam name="T">Entity type</typeparam>
        void Remove<T>(T entity) where T : class, IEntity;
    }
}
