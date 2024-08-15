using ApplicationLayer.VisaApplications.Requests;
using Domains.VisaApplicationDomain;

namespace ApplicationLayer.VisaApplications.Handlers;

public interface IVisaApplicationsRequestHandler
{
    Task<List<VisaApplication>> Get(CancellationToken cancellationToken);

    void HandleCreateRequest(VisaApplicationCreateRequest request, CancellationToken cancellationToken);
}
