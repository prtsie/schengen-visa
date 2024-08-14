namespace Domains
{
    /// Interface that every entity should inherit from
    public interface IEntity
    {
        public Guid Id { get; }
    }
}
