using Domains.LocationDomain;
using Infrastructure.Database.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Locations.Repositories.Countries
{
    public class CountriesRepository(IGenericReader reader, IGenericWriter writer, IUnitOfWork unitOfWork)
        : GenericRepository<Country>(writer, unitOfWork), ICountriesRepository
    {
        protected override IQueryable<Country> LoadDomain()
        {
            return reader.GetAll<Country>().Include(c => c.Cities);
        }
    }
}
