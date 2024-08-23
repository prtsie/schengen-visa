using ApplicationLayer.Services.Applicants.NeededServices;
using Domains.ApplicantDomain;
using Infrastructure.Database.Applicants.Repositories.Exceptions;
using Infrastructure.Database.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Applicants.Repositories;

/// Repository pattern for <see cref="Applicant"/>
/// <param name="reader"><inheritdoc cref="IGenericReader"/></param>
/// <param name="writer"><inheritdoc cref="IGenericWriter"/></param>
public sealed class ApplicantsRepository(IGenericReader reader, IGenericWriter writer)
    : GenericRepository<Applicant>(reader, writer), IApplicantsRepository
{
    protected override IQueryable<Applicant> LoadDomain()
    {
        return base.LoadDomain()
            .Include(a => a.PlaceOfWork);
    }

    async Task<Applicant> IApplicantsRepository.FindByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var result = await LoadDomain().SingleOrDefaultAsync(a => a.UserId == userId, cancellationToken);
        return result ?? throw new ApplicantNotFoundByUserIdException();
    }

    async Task<Guid> IApplicantsRepository.GetApplicantIdByUserId(Guid userId, CancellationToken cancellationToken)
    {
        var result = await base.LoadDomain().SingleOrDefaultAsync(a => a.UserId == userId, cancellationToken);
        return result?.Id ?? throw new ApplicantNotFoundByUserIdException();
    }
}
