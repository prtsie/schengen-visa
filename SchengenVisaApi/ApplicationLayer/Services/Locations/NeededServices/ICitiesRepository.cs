using ApplicationLayer.InfrastructureServicesInterfaces;
using Domains.LocationDomain;

namespace ApplicationLayer.Services.Locations.NeededServices;

public interface ICitiesRepository : IGenericRepository<City>
{
    /// Get <see cref="City"/> by name and country identifier
    Task<City?> GetByNameAsync(Guid requestId, string existingCity, CancellationToken cancellationToken);
}
