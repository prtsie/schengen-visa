using Domains.LocationDomain;
using Infrastructure.Database.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Locations.Repositories.Cities
{
    public class CitiesRepository(IGenericReader reader, IGenericWriter writer, IUnitOfWork unitOfWork)
        : GenericRepository<City>(writer, unitOfWork), ICitiesRepository
    {
        protected override IQueryable<City> LoadDomain()
        {
            return reader.GetAll<City>().Include(c => c.Country);
        }
    }
}
