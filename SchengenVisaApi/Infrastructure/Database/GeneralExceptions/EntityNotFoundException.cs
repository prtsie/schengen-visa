using Domains;

namespace Infrastructure.Database.GeneralExceptions
{
    /// Exception to throw when entity with specific id not found
    /// <param name="id">Identifier of entity</param>
    /// <typeparam name="T">Not found entity type</typeparam>
    public class EntityNotFoundException<T>(Guid id) : Exception($"Entity {typeof(T).Name} with id '{id}' not found")
        where T : class, IEntity;
}
