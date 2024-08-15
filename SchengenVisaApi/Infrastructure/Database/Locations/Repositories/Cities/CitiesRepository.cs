using ApplicationLayer.Locations;
using Domains.LocationDomain;
using Infrastructure.Database.Generic;
using Infrastructure.Database.Locations.Repositories.Cities.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Locations.Repositories.Cities;

public sealed class CitiesRepository(IGenericReader reader, IGenericWriter writer, IUnitOfWork unitOfWork)
    : GenericRepository<City>(reader, writer, unitOfWork), ICitiesRepository
{
    protected override IQueryable<City> LoadDomain()
    {
        return base.LoadDomain().Include(c => c.Country);
    }

    ///<inheritdoc cref="ICitiesRepository.GetByNameAsync"/>
    /// <exception cref="CityNotFoundByNameException">city with provided name and country not found</exception>
    public async Task<City> GetByNameAsync(string name, string countryName, CancellationToken cancellationToken)
    {
        var result = await LoadDomain().Where(c => c.Name == name && c.Country.Name == countryName).SingleOrDefaultAsync(cancellationToken);
        return result ?? throw new CityNotFoundByNameException(name, countryName);
    }
}
