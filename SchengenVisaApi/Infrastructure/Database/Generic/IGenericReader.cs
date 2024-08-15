using Domains;

namespace Infrastructure.Database.Generic
{
    /// Reads from data storage
    public interface IGenericReader
    {
        /// Get all entities of type T stored in storage
        /// <typeparam name="T">Entity type to seek in storage</typeparam>
        IQueryable<T> GetAll<T>() where T : class, IEntity;
    }
}
