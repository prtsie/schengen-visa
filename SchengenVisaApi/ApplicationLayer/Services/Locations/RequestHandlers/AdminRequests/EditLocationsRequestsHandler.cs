using ApplicationLayer.GeneralNeededServices;
using ApplicationLayer.Services.Locations.NeededServices;
using ApplicationLayer.Services.Locations.RequestHandlers.AdminRequests.Exceptions;
using ApplicationLayer.Services.Locations.Requests;
using Domains.LocationDomain;

namespace ApplicationLayer.Services.Locations.RequestHandlers.AdminRequests
{
    /// <inheritdoc cref="IEditLocationsRequestsHandler"/>
    public class EditLocationsRequestsHandler(ICountriesRepository countries, IUnitOfWork unitOfWork) : IEditLocationsRequestsHandler
    {
        async Task IEditLocationsRequestsHandler.AddCountryAsync(AddCountryRequest request, CancellationToken cancellationToken)
        {
            if (await countries.FindByName(request.CountryName, cancellationToken) is not null)
            {
                throw new CountryAlreadyExists(request.CountryName);
            }

            if (request.Cities.Distinct().Count() < request.Cities.Length)
            {
                throw new MultipleIdenticalCitiesInCountryException();
            }

            var country = new Country
            {
                Name = request.CountryName,
                IsSchengen = request.IsSchengen,
                Cities = request.Cities.Select(cityName => new City { Name = cityName }).ToList()
            };

            await countries.AddAsync(country, cancellationToken);

            await unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
