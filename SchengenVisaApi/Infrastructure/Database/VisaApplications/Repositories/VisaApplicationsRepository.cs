using Domains.VisaApplicationDomain;
using Infrastructure.Database.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.VisaApplications.Repositories
{
    public class VisaApplicationsRepository(IGenericReader reader, IGenericWriter writer, IUnitOfWork unitOfWork)
        : GenericRepository<VisaApplication>(writer, unitOfWork), IVisaApplicationsRepository
    {
        protected override IQueryable<VisaApplication> LoadDomain()
        {
            return reader.GetAll<VisaApplication>()
                .Include(a => a.DestinationCountry)
                .Include(a => a.PastVisas)
                .Include(a => a.PastVisits);
        }
    }
}
