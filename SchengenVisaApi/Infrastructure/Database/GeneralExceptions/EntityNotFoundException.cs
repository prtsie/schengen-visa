namespace Infrastructure.Database.GeneralExceptions
{
    public class EntityNotFoundException<T>(Guid id) : Exception($"Entity {typeof(T).Name} with id '{id}' not found");
}
