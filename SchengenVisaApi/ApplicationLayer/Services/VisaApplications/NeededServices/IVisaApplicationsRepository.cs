using ApplicationLayer.InfrastructureServicesInterfaces;
using Domains.VisaApplicationDomain;

namespace ApplicationLayer.Services.VisaApplications.NeededServices;

public interface IVisaApplicationsRepository : IGenericRepository<VisaApplication>
{
    /// Get applications of one applicant
    Task<List<VisaApplication>> GetOfApplicantAsync(Guid applicantId, CancellationToken cancellationToken);

    /// Get specific application of specific user
    Task<VisaApplication> GetByApplicantAndApplicationIdAsync(Guid applicantId, Guid applicationId, CancellationToken cancellationToken);

    /// Returns pending applications for approving authorities
    Task<List<VisaApplication>> GetPendingApplicationsAsync(CancellationToken cancellationToken);
}
