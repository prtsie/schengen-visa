using System.Reflection;
using Domains;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext, IWriter, IReader, IUnitOfWork
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        async Task IWriter.AddAsync<T>(T entity, CancellationToken cancellationToken)
        {
            await AddAsync(entity, cancellationToken);
        }

        void IWriter.Update<T>(T entity)
        {
            Update(entity);
        }

        void IWriter.Remove<T>(T entity)
        {
            Remove(entity);
        }

        IQueryable<T> IReader.GetAll<T>()
        {
            return Set<T>();
        }

        async Task<T?> IReader.GetOneAsync<T>(Guid id, CancellationToken cancellationToken)
            where T : class
        {
            return await Set<T>().FindAsync([id], cancellationToken: cancellationToken);
        }

        async Task IUnitOfWork.SaveAsync(CancellationToken cancellationToken)
        {
            await SaveChangesAsync(cancellationToken);
        }
    }
}
