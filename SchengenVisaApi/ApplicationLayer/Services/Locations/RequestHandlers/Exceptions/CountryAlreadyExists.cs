using ApplicationLayer.GeneralExceptions;

namespace ApplicationLayer.Services.Locations.RequestHandlers.Exceptions
{
    public class CountryAlreadyExists(string countryName) : AlreadyExistsException($"{countryName} already exists.");
}
