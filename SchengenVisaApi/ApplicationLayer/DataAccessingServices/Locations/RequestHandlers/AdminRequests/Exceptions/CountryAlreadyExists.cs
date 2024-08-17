using ApplicationLayer.GeneralExceptions;

namespace ApplicationLayer.DataAccessingServices.Locations.RequestHandlers.AdminRequests.Exceptions
{
    public class CountryAlreadyExists(string countryName) : AlreadyExistsException($"{countryName} already exists.");
}
