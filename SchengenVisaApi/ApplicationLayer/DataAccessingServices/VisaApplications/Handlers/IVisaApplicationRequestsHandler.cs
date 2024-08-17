using ApplicationLayer.DataAccessingServices.VisaApplications.Requests;
using Domains.VisaApplicationDomain;

namespace ApplicationLayer.DataAccessingServices.VisaApplications.Handlers;

public interface IVisaApplicationRequestsHandler
{
    Task<List<VisaApplication>> Get(CancellationToken cancellationToken);

    void HandleCreateRequest(Guid userId, VisaApplicationCreateRequest request, CancellationToken cancellationToken);
}
