using ApplicationLayer.GeneralExceptions;

namespace ApplicationLayer.Services.Locations.RequestHandlers.Exceptions
{
    public class MultipleIdenticalCitiesInCountryException() : ApiException("There are multiple cities with one name in the country.");
}
