using Domains.ApplicantDomain;
using Infrastructure.Database.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Applicants.Repositories
{
    public class ApplicantsRepository(IGenericReader reader, IGenericWriter writer, IUnitOfWork unitOfWork)
        : GenericRepository<Applicant>(writer, unitOfWork), IApplicantsRepository
    {
        protected override IQueryable<Applicant> LoadDomain()
        {
            return reader.GetAll<Applicant>()
                .Include(a => a.CountryOfBirth)
                .Include(a => a.CityOfBirth)
                .Include(a => a.PlaceOfWork);
        }
    }
}
