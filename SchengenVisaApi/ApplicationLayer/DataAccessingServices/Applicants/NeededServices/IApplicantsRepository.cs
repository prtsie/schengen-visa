using ApplicationLayer.GeneralNeededServices;
using Domains.ApplicantDomain;

namespace ApplicationLayer.DataAccessingServices.Applicants.NeededServices;

/// Repository pattern for <see cref="Applicant"/>
public interface IApplicantsRepository : IGenericRepository<Applicant>
{
    /// Find <see cref="Applicant"/> by Identifier
    Task<Applicant> FindByUserIdAsync(Guid userId, CancellationToken cancellationToken);
}
