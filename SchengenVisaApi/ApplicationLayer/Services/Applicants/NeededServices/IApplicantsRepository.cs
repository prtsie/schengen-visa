using ApplicationLayer.InfrastructureServicesInterfaces;
using Domains.ApplicantDomain;

namespace ApplicationLayer.Services.Applicants.NeededServices;

/// Repository pattern for <see cref="Applicant"/>
public interface IApplicantsRepository : IGenericRepository<Applicant>
{
    /// Find <see cref="Applicant"/> by Identifier
    Task<Applicant> FindByUserIdAsync(Guid userId, CancellationToken cancellationToken);

    /// Get identifier of applicant by user identifier
    Task<Guid> GetApplicantIdByUserId(Guid userId, CancellationToken cancellationToken);
}
