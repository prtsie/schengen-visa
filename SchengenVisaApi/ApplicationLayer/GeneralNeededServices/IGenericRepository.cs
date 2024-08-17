using Domains;

namespace ApplicationLayer.GeneralNeededServices;

/// <summary>
/// Generic repository pattern
/// </summary>
/// <typeparam name="T">Entity type</typeparam>
public interface IGenericRepository<T> where T : class, IEntity
{
    /// Get all entities from data storage
    /// <param name="cancellationToken">Cancellation token</param>
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken);

    /// Get one entity with specific id
    /// <param name="id">Identifier of entity</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// Add entity to storage
    /// <param name="entity">Entity to add</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task AddAsync(T entity, CancellationToken cancellationToken);

    /// <summary>
    /// Update entity in storage
    /// </summary>
    /// <param name="entity">Entity to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task UpdateAsync(T entity, CancellationToken cancellationToken);

    /// <summary>
    /// Remove entity from storage
    /// </summary>
    /// <param name="entity">Entity to remove</param>
    void Remove(T entity);
}
