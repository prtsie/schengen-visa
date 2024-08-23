using ApplicationLayer.Services.VisaApplications.NeededServices;
using Domains.VisaApplicationDomain;
using Infrastructure.Database.Generic;
using Infrastructure.Database.VisaApplications.Repositories.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.VisaApplications.Repositories;

public sealed class VisaApplicationsRepository(IGenericReader reader, IGenericWriter writer)
    : GenericRepository<VisaApplication>(reader, writer), IVisaApplicationsRepository
{
    protected override IQueryable<VisaApplication> LoadDomain()
        => base.LoadDomain()
            .Include(va => va.PastVisas)
            .Include(va => va.PastVisits);


    async Task<List<VisaApplication>> IVisaApplicationsRepository.GetOfApplicantAsync(Guid applicantId, CancellationToken cancellationToken)
        => await LoadDomain().Where(va => va.ApplicantId == applicantId).ToListAsync(cancellationToken);

    async Task<VisaApplication> IVisaApplicationsRepository.GetByApplicantAndApplicationIdAsync(
        Guid applicantId,
        Guid applicationId,
        CancellationToken cancellationToken)
    {
        var result = await LoadDomain()
            .SingleOrDefaultAsync(va => va.Id == applicationId && va.ApplicantId == applicantId, cancellationToken);
        return result ?? throw new ApplicationNotFoundByApplicantAndApplicationIdException(applicationId);
    }
}
