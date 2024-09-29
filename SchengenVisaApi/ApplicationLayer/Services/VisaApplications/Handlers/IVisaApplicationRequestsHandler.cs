using ApplicationLayer.Services.VisaApplications.Models;
using ApplicationLayer.Services.VisaApplications.Requests;

namespace ApplicationLayer.Services.VisaApplications.Handlers;

public interface IVisaApplicationRequestsHandler
{
    /// Returns all applications for approving authorities
    Task<List<VisaApplicationPreview>> GetPendingAsync(CancellationToken cancellationToken);

    /// Returns all applications of one applicant
    Task<List<VisaApplicationPreview>> GetForApplicantAsync(CancellationToken cancellationToken);

    /// Returns one application with specific id
    Task<VisaApplicationModel> GetApplicationForApplicantAsync(Guid id, CancellationToken cancellationToken);

    /// Returns one application with specific id
    Task<VisaApplicationModel> GetApplicationForAuthorityAsync(Guid id, CancellationToken cancellationToken);

    /// Creates application for applicant with specific user identifier
    Task HandleCreateRequestAsync(VisaApplicationCreateRequest request, CancellationToken cancellationToken);

    /// Sets application status to closed
    Task HandleCloseRequestAsync(Guid applicationId, CancellationToken cancellationToken);

    /// Sets application status to approved or rejected
    Task SetApplicationStatusFromAuthorityAsync(Guid applicationId, AuthorityRequestStatuses status, CancellationToken cancellationToken);

    /// Returns stream with file with formatted application data to download
    Task<Stream> ApplicationToStreamAsync(Guid applicationId, CancellationToken cancellationToken);
}
