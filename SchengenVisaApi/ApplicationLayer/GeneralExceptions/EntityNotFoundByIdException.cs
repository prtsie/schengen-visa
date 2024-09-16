using Domains;

namespace ApplicationLayer.GeneralExceptions;

/// Exception to throw when entity not found
/// <param name="id">Identifier of entity</param>
/// <typeparam name="T">Type of entity</typeparam>
public class EntityNotFoundByIdException<T>(Guid id) : EntityNotFoundException($"{typeof(T).Name} with id {id} not found.")
    where T : class, IEntity;
