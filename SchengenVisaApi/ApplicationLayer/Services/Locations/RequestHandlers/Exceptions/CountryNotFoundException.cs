using ApplicationLayer.Services.GeneralExceptions;
using Domains.LocationDomain;

namespace ApplicationLayer.Services.Locations.RequestHandlers.Exceptions
{
    public class CountryNotFoundException(string countryName) : EntityNotFoundException<Country>($"Country {countryName} is not supported.");
}
