using Domains.ApplicantDomain;
using Infrastructure.Database.Generic;

namespace Infrastructure.Database.Applicants.Repositories
{
    /// Repository pattern for <see cref="Applicant"/>
    public interface IApplicantsRepository : IGenericRepository<Applicant> { }
}
