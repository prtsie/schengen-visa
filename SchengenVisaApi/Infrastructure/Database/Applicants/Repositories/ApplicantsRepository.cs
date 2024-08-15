using ApplicationLayer.Applicants;
using ApplicationLayer.Common;
using Domains.ApplicantDomain;
using Infrastructure.Database.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Applicants.Repositories;

/// Repository pattern for <see cref="Applicant"/>
/// <param name="reader"><inheritdoc cref="IGenericReader"/></param>
/// <param name="writer"><inheritdoc cref="IGenericWriter"/></param>
/// <param name="unitOfWork"><inheritdoc cref="IUnitOfWork"/></param>
public sealed class ApplicantsRepository(IGenericReader reader, IGenericWriter writer, IUnitOfWork unitOfWork)
    : GenericRepository<Applicant>(reader, writer, unitOfWork), IApplicantsRepository
{
    protected override IQueryable<Applicant> LoadDomain()
    {
        return base.LoadDomain()
            .Include(a => a.CountryOfBirth)
            .Include(a => a.CityOfBirth)
            .Include(a => a.PlaceOfWork);
    }
}