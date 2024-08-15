using ApplicationLayer.Common;
using Domains.LocationDomain;

namespace ApplicationLayer.Locations;

public interface ICitiesRepository : IGenericRepository<City>
{
    /// Find the city by its name and its country name
    /// <param name="name">City's name</param>
    /// <param name="countryName">City's country name</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>City with specific name and country</returns>
    Task<City> GetByNameAsync(string name, string countryName, CancellationToken cancellationToken);
}
