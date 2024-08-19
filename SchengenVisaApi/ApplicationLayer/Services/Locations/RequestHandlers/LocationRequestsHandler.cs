using ApplicationLayer.GeneralNeededServices;
using ApplicationLayer.Services.Locations.NeededServices;
using ApplicationLayer.Services.Locations.RequestHandlers.Exceptions;
using ApplicationLayer.Services.Locations.Requests;
using Domains.LocationDomain;

namespace ApplicationLayer.Services.Locations.RequestHandlers
{
    /// <inheritdoc cref="ILocationRequestsHandler"/>
    public class LocationRequestsHandler(ICountriesRepository countries, IUnitOfWork unitOfWork) : ILocationRequestsHandler
    {
        async Task<List<Country>> ILocationRequestsHandler.HandleGetRequestAsync(CancellationToken cancellationToken)
        {
            return await countries.GetAllAsync(cancellationToken);
        }

        async Task ILocationRequestsHandler.AddCountryAsync(AddCountryRequest request, CancellationToken cancellationToken)
        {
            if (await countries.FindByName(request.CountryName, cancellationToken) is not null)
            {
                throw new CountryAlreadyExists(request.CountryName);
            }

            if (request.Cities.Distinct().Count() < request.Cities.Length)
            {
                throw new MultipleIdenticalCitiesInCountryException();
            }

            //todo mapper
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
