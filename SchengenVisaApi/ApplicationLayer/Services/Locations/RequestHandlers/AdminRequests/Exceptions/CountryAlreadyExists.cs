using ApplicationLayer.GeneralExceptions;

namespace ApplicationLayer.Services.Locations.RequestHandlers.AdminRequests.Exceptions
{
    public class CountryAlreadyExists(string countryName) : AlreadyExistsException($"{countryName} already exists.");
}
