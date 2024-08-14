using Domains;
using Infrastructure.Database.GeneralExceptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Generic
{
    public abstract class GenericRepository<T>(IGenericWriter writer, IUnitOfWork unitOfWork) : IGenericRepository<T>
        where T : class, IEntity
    {
        public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
            => await LoadDomain().ToListAsync(cancellationToken);

        public async Task<T> GetOneAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await LoadDomain().SingleOrDefaultAsync(a => a.Id == id, cancellationToken);
            return result ?? throw new EntityNotFoundException<T>(id);
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken)
            => await writer.AddAsync(entity, cancellationToken);

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            await GetOneAsync(entity.Id, cancellationToken);
            writer.Update(entity);
        }

        public void Remove(T entity)
        {
            writer.Remove(entity);
        }

        public async Task SaveAsync(CancellationToken cancellationToken)
            => await unitOfWork.SaveAsync(cancellationToken);

        protected abstract IQueryable<T> LoadDomain();
    }
}
