namespace Domains.Common
{
    /// Model of a city
    public class City : IEntity
    {
        /// Unique identifier of the city
        public Guid Id { get; private set; } = Guid.NewGuid();

        /// Name of the city
        public string Name { get; set; } = null!;

        /// <see cref="Domains.Common.Country"/> in which the city is located
        public Country Country { get; set; } = null!;
    }
}
