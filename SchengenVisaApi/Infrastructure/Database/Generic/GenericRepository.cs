using ApplicationLayer.Common;
using Domains;
using Infrastructure.Database.GeneralExceptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Generic;

/// Generic repository pattern
/// <param name="writer"><inheritdoc cref="IGenericWriter"/></param>
/// <param name="unitOfWork"><inheritdoc cref="IUnitOfWork"/></param>
/// <typeparam name="T">Type of entity</typeparam>
/// <remarks>Should be inherited to create typed repositories</remarks>
public abstract class GenericRepository<T>(IGenericReader reader, IGenericWriter writer, IUnitOfWork unitOfWork) : IGenericRepository<T>
    where T : class, IEntity
{
    /// <inheritdoc cref="IGenericRepository{T}.GetAllAsync"/>
    public async Task<List<T>> GetAllAsync(CancellationToken cancellationToken)
        => await LoadDomain().ToListAsync(cancellationToken);

    /// <inheritdoc cref="IGenericRepository{T}.GetOneAsync"/>
    public async Task<T> GetOneAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await LoadDomain().SingleOrDefaultAsync(a => a.Id == id, cancellationToken);
        return result ?? throw new EntityNotFoundException<T>(id);
    }

    /// <inheritdoc cref="IGenericRepository{T}.AddAsync"/>
    public async Task AddAsync(T entity, CancellationToken cancellationToken)
        => await writer.AddAsync(entity, cancellationToken);

    /// <inheritdoc cref="IGenericRepository{T}.UpdateAsync"/>
    public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        await GetOneAsync(entity.Id, cancellationToken);
        writer.Update(entity);
    }

    /// <inheritdoc cref="IGenericRepository{T}.Remove"/>
    public void Remove(T entity)
    {
        writer.Remove(entity);
    }

    /// <inheritdoc cref="IGenericRepository{T}.SaveAsync"/>
    public async Task SaveAsync(CancellationToken cancellationToken)
        => await unitOfWork.SaveAsync(cancellationToken);

    /// Should be overriden to load navigation properties of entity
    protected virtual IQueryable<T> LoadDomain()
    {
        return reader.GetAll<T>();
    }
}