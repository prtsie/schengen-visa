using Domains.LocationDomain;
using Infrastructure.Database.GeneralExceptions;

namespace Infrastructure.Database.Locations.Repositories.Cities.Exceptions
{
    /// Exception to throw when city cannot be found by its name and its country name
    /// <param name="name">Name of the city</param>
    /// <param name="countryName">name of the city's country</param>
    public class CityNotFoundByNameException(string name, string countryName)
        : EntityNotFoundException<City>($"{name} with country {countryName} not found.");
}
