using ApplicationLayer.InfrastructureServicesInterfaces;
using Domains.LocationDomain;

namespace ApplicationLayer.Services.Locations.NeededServices;

public interface ICountriesRepository : IGenericRepository<Country>
{
    /// Gets country by name
    Task<Country?> FindByNameAsync(string countryName, CancellationToken cancellationToken);

    /// Gets country by identifier
    Task<Country?> FindByIdAsync(Guid id, CancellationToken cancellationToken);
}
