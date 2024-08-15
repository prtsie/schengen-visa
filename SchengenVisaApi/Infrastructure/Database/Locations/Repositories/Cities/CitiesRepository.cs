using Domains.LocationDomain;
using Infrastructure.Database.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Locations.Repositories.Cities;

public sealed class CitiesRepository(IGenericReader reader, IGenericWriter writer, IUnitOfWork unitOfWork)
    : GenericRepository<City>(reader, writer, unitOfWork), ICitiesRepository
{
    protected override IQueryable<City> LoadDomain()
    {
        return base.LoadDomain().Include(c => c.Country);
    }
}