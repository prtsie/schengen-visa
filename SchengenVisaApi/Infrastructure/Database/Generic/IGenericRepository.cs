using Domains;

namespace Infrastructure.Database.Generic
{
    public interface IGenericRepository<T> where T : class, IEntity
    {
        Task<List<T>> GetAllAsync(CancellationToken cancellationToken);

        Task<T> GetOneAsync(Guid id, CancellationToken cancellationToken);

        Task AddAsync(T entity, CancellationToken cancellationToken);

        Task UpdateAsync(T entity, CancellationToken cancellationToken);

        void Remove(T entity);

        Task SaveAsync(CancellationToken cancellationToken);
    }
}
