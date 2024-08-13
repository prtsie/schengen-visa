using Domains;

namespace Infrastructure.Database
{
    public interface IReader
    {
        /// Get all entities of type <typeparamref name="T"/> stored in storage
        /// <typeparam name="T">Entity type to seek in storage</typeparam>
        IQueryable<T> GetAll<T>() where T : class, IEntity;

        /// Get one entity with specific <paramref name="id"/> from storage
        /// <param name="id">Identifier of entity</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <typeparam name="T">Type of entity</typeparam>
        /// <returns>Entity or null if not found</returns>
        Task<T?> GetOneAsync<T>(Guid id, CancellationToken cancellationToken) where T : class, IEntity;
    }
}
