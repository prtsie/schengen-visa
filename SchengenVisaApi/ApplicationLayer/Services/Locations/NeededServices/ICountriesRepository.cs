using ApplicationLayer.GeneralNeededServices;
using Domains.LocationDomain;

namespace ApplicationLayer.Services.Locations.NeededServices;

public interface ICountriesRepository : IGenericRepository<Country>
{
    /// Gets country by name
    /// <param name="countryName">Name of country to seek</param>
    /// <param name="cancellationToken">Cancellation Token</param>
    /// <returns>Country or null if not found</returns>
    Task<Country?> FindByName(string countryName, CancellationToken cancellationToken);
}
