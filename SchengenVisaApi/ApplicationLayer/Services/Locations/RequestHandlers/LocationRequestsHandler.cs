using ApplicationLayer.InfrastructureServicesInterfaces;
using ApplicationLayer.Services.Applicants.NeededServices;
using ApplicationLayer.Services.Locations.NeededServices;
using ApplicationLayer.Services.Locations.RequestHandlers.Exceptions;
using ApplicationLayer.Services.Locations.Requests;
using Domains.LocationDomain;

namespace ApplicationLayer.Services.Locations.RequestHandlers
{
    /// <inheritdoc cref="ILocationRequestsHandler"/>
    public class LocationRequestsHandler(
        ICountriesRepository countries,
        ICitiesRepository cities,
        IApplicantsRepository applicants,
        IUnitOfWork unitOfWork) : ILocationRequestsHandler
    {
        async Task<List<Country>> ILocationRequestsHandler.HandleGetRequestAsync(CancellationToken cancellationToken)
        {
            return await countries.GetAllAsync(cancellationToken);
        }

        async Task ILocationRequestsHandler.AddCountryAsync(AddCountryRequest request, CancellationToken cancellationToken)
        {
            if (await countries.FindByNameAsync(request.CountryName, cancellationToken) is not null)
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

        async Task ILocationRequestsHandler.UpdateCountryAsync(UpdateCountryRequest request, CancellationToken cancellationToken)
        {
            if (await countries.FindByNameAsync(request.CountryName, cancellationToken) is not null)
            {
                throw new CountryAlreadyExists(request.CountryName);
            }

            var country = await countries.FindByIdAsync(request.Id, cancellationToken);
            if (country is null)
            {
                throw new CountryNotFoundException(request.CountryName);
            }

            var existingCities = country.Cities;
            var citiesToAdd = request.Cities.Except(existingCities.Select(c => c.Name)).ToList();
            var citiesToRemove = existingCities.Where(c => !request.Cities.Contains(c.Name));
            var applicantsList = await applicants.GetAllAsync(cancellationToken);

            //todo mapper
            foreach (var city in citiesToRemove)
            {
                if (applicantsList.All(a => a.CityOfBirth.Id != city.Id && a.PlaceOfWork.Address.City.Id != city.Id))
                {
                    cities.Remove(city);
                }
                else
                {
                    throw new CityCanNotBeDeletedException(city.Name);
                }
            }

            foreach (var city in citiesToAdd)
            {
                await cities.AddAsync(new City { Name = city, Country = country }, cancellationToken);
            }

            country.Name = request.CountryName;
            country.IsSchengen = request.IsSchengen;

            await countries.UpdateAsync(country, cancellationToken);

            await unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
