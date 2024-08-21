using ApplicationLayer.Services.GeneralExceptions;

namespace ApplicationLayer.Services.Locations.RequestHandlers.Exceptions
{
    public class CityCanNotBeDeletedException(string cityName)
        : EntityUsedInDatabaseException($"{cityName} can not be deleted because some applicants live or work in it");
}
