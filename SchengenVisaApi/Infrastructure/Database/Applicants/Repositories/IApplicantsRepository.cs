using Domains.ApplicantDomain;
using Infrastructure.Database.Generic;

namespace Infrastructure.Database.Applicants.Repositories
{
    public interface IApplicantsRepository : IGenericRepository<Applicant> { }
}
