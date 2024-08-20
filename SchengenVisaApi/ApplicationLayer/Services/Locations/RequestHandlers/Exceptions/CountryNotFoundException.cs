using Domains.LocationDomain;
using Infrastructure.Database.GeneralExceptions;

namespace ApplicationLayer.Services.Locations.RequestHandlers.Exceptions
{
    public class CountryNotFoundException(string countryName) : EntityNotFoundException<Country>($"Country {countryName} is not supported.");
}
