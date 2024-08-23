using ApplicationLayer.Services.VisaApplications.Models;
using ApplicationLayer.Services.VisaApplications.Requests;

namespace ApplicationLayer.Services.VisaApplications.Handlers;

public interface IVisaApplicationRequestsHandler
{
    /// Returns all applications for approving authorities
    Task<List<VisaApplicationModelForAuthority>> GetAllAsync(CancellationToken cancellationToken);

    /// Returns all applications of one applicant
    Task<List<VisaApplicationModelForApplicant>> GetForApplicantAsync(Guid userId, CancellationToken cancellationToken);

    /// Creates application for applicant with specific user identifier
    Task HandleCreateRequestAsync(Guid userId, VisaApplicationCreateRequest request, CancellationToken cancellationToken);

    /// Sets application status to closed
    Task HandleCloseRequestAsync(Guid userId, Guid applicationId, CancellationToken cancellationToken);

    Task SetApplicationStatusFromAuthorityAsync(Guid applicationId, AuthorityRequestStatuses status, CancellationToken cancellationToken);
}
