namespace Domains.Common
{
    /// Model of a country
    public class Country : IEntity
    {
        /// Name of the country
        public string Name { get; set; } = null!;

        /// Located in Schengen area
        public bool IsSchengen { get; set; }

        /// List of <see cref="City"/> that country have
        public List<City> Cities { get; set; } = null!;
    }
}
