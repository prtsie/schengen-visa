using ApplicationLayer.Services.VisaApplications.NeededServices;
using Domains.VisaApplicationDomain;
using Infrastructure.Database.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.VisaApplications.Repositories;

public sealed class VisaApplicationsRepository(IGenericReader reader, IGenericWriter writer)
    : GenericRepository<VisaApplication>(reader, writer), IVisaApplicationsRepository
{
    protected override IQueryable<VisaApplication> LoadDomain()
        => base.LoadDomain()
            .Include(va => va.DestinationCountry)
            .Include(va => va.PastVisas)
            .Include(va => va.PastVisits);


    async Task<List<VisaApplication>> IVisaApplicationsRepository.GetOfApplicantAsync(Guid applicantId, CancellationToken cancellationToken)
        => await LoadDomain().Where(va => va.ApplicantId == applicantId).ToListAsync(cancellationToken);
}
