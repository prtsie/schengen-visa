using ApplicationLayer.GeneralExceptions;

namespace ApplicationLayer.Services.Locations.RequestHandlers.AdminRequests.Exceptions
{
    public class MultipleIdenticalCitiesInCountryException() : ApiException("There are multiple cities with one name in the country.");
}
