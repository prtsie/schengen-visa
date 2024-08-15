namespace Domains.LocationDomain;

/// Model of a country
public class Country : IEntity
{
    /// Unique identifier of the <see cref="Country"/>
    public Guid Id { get; private set; } = Guid.NewGuid();

    /// Name of the country
    public string Name { get; set; } = null!;

    /// Located in Schengen area
    public bool IsSchengen { get; set; }

    /// List of <see cref="City"/> that country have
    public List<City> Cities { get; set; } = null!;
}