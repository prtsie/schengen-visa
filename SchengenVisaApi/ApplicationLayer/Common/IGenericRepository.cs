using Domains;

namespace ApplicationLayer.Common;

/// <summary>
/// Generic repository pattern
/// </summary>
/// <typeparam name="T">Entity type</typeparam>
public interface IGenericRepository<T> where T : class, IEntity
{
    /// Get all entities from data storage
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken);

    /// Get one entity with specific id
    /// <param name="id">Identifier of entity</param>
    Task<T> GetOneAsync(Guid id, CancellationToken cancellationToken);

    /// Add entity to storage
    /// <param name="entity">Entity to add</param>
    Task AddAsync(T entity, CancellationToken cancellationToken);

    /// <summary>
    /// Update entity in storage
    /// </summary>
    /// <param name="entity">Entity to update</param>
    Task UpdateAsync(T entity, CancellationToken cancellationToken);

    /// <summary>
    /// Remove entity from storage
    /// </summary>
    /// <param name="entity">Entity to remove</param>
    void Remove(T entity);

    /// Save changes in storage
    Task SaveAsync(CancellationToken cancellationToken);
}
