using ApplicationLayer.Services.VisaApplications.NeededServices;
using Domains.VisaApplicationDomain;
using Infrastructure.Database.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.VisaApplications.Repositories;

public sealed class VisaApplicationsRepository(IGenericReader reader, IGenericWriter writer)
    : GenericRepository<VisaApplication>(reader, writer), IVisaApplicationsRepository
{
    protected override IQueryable<VisaApplication> LoadDomain()
    {
        return base.LoadDomain()
            .Include(a => a.DestinationCountry)
            .Include(a => a.PastVisas)
            .Include(a => a.PastVisits);
    }
}
