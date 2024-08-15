using Domains;

namespace Infrastructure.Database.GeneralExceptions;

/// Exception to throw when entity not found
/// <param name="id">Identifier of entity</param>
/// <typeparam name="T">Type of entity</typeparam>
public class EntityNotFoundByIdException<T>(Guid id) : EntityNotFoundException<T>($"Entity {typeof(T).Name} with id {id} not found.")
    where T : class, IEntity;