using ApplicationLayer.Services.Locations.NeededServices;
using Domains.LocationDomain;
using Infrastructure.Database.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Locations.Repositories.Cities;

public sealed class CitiesRepository(IGenericReader reader, IGenericWriter writer)
    : GenericRepository<City>(reader, writer), ICitiesRepository
{
    protected override IQueryable<City> LoadDomain()
    {
        return base.LoadDomain().Include(c => c.Country);
    }

    Task<City?> ICitiesRepository.GetByNameAsync(Guid countryId, string cityName, CancellationToken cancellationToken)
        => LoadDomain().SingleOrDefaultAsync(c => c.Country.Id == countryId && c.Name == cityName, cancellationToken);
}
