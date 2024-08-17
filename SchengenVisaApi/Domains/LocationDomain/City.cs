using System.Text.Json.Serialization;

namespace Domains.LocationDomain;

/// Model of a city
public class City : IEntity
{
    /// Unique identifier of the <see cref="City"/>
    public Guid Id { get; private set; } = Guid.NewGuid();

    /// Name of the city
    public string Name { get; set; } = null!;

    /// <see cref="LocationDomain.Country"/> in which the city is located
    [JsonIgnore]
    public Country Country { get; set; } = null!;
}
