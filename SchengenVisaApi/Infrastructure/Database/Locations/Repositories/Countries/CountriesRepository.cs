using ApplicationLayer.Services.Locations.NeededServices;
using Domains.LocationDomain;
using Infrastructure.Database.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Locations.Repositories.Countries;

public sealed class CountriesRepository(IGenericReader reader, IGenericWriter writer)
    : GenericRepository<Country>(reader, writer), ICountriesRepository
{
    protected override IQueryable<Country> LoadDomain()
    {
        return base.LoadDomain().Include(c => c.Cities);
    }

    async Task<Country?> ICountriesRepository.FindByNameAsync(string countryName, CancellationToken cancellationToken)
    {
        var result = await LoadDomain().SingleOrDefaultAsync(c => c.Name == countryName, cancellationToken);
        return result;
    }

    async Task<Country?> ICountriesRepository.FindByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await LoadDomain().SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
        return result;
    }
}
