using Domains.LocationDomain;
using Infrastructure.Database.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Locations.Repositories.Countries
{
    public sealed class CountriesRepository(IGenericReader reader, IGenericWriter writer, IUnitOfWork unitOfWork)
        : GenericRepository<Country>(reader, writer, unitOfWork), ICountriesRepository
    {
        protected override IQueryable<Country> LoadDomain()
        {
            return base.LoadDomain().Include(c => c.Cities);
        }
    }
}
