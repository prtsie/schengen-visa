using ApplicationLayer.Services.VisaApplications.Models;
using ApplicationLayer.Services.VisaApplications.Requests;
using Domains.VisaApplicationDomain;

namespace ApplicationLayer.Services.VisaApplications.Handlers;

public interface IVisaApplicationRequestsHandler
{
    Task<List<VisaApplication>> Get(CancellationToken cancellationToken);
    Task<List<VisaApplicationModelForApplicant>> GetForApplicant(Guid userId, CancellationToken cancellationToken);

    Task HandleCreateRequest(Guid userId, VisaApplicationCreateRequest request, CancellationToken cancellationToken);
}
