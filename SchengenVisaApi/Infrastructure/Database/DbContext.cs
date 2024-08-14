using System.Reflection;
using Infrastructure.Database.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database
{
    public class DbContext : Microsoft.EntityFrameworkCore.DbContext, IGenericWriter, IGenericReader, IUnitOfWork
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        async Task IGenericWriter.AddAsync<T>(T entity, CancellationToken cancellationToken)
        {
            await AddAsync(entity, cancellationToken);
        }

        void IGenericWriter.Update<T>(T entity)
        {
            Update(entity);
        }

        void IGenericWriter.Remove<T>(T entity)
        {
            Remove(entity);
        }

        IQueryable<T> IGenericReader.GetAll<T>()
        {
            return Set<T>();
        }

        async Task IUnitOfWork.SaveAsync(CancellationToken cancellationToken)
        {
            await SaveChangesAsync(cancellationToken);
        }
    }
}
