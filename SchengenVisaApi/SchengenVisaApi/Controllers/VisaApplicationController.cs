using ApplicationLayer.VisaApplications.Handlers;
using ApplicationLayer.VisaApplications.Requests;
using Microsoft.AspNetCore.Mvc;

namespace SchengenVisaApi.Controllers;

[ApiController]
[Route("[controller]")]
public class VisaApplicationController(IVisaApplicationsRequestHandler visaApplicationsRequestHandler) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var result = await visaApplicationsRequestHandler.Get(cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public void Create(VisaApplicationCreateRequest request, CancellationToken cancellationToken)
    {
        visaApplicationsRequestHandler.HandleCreateRequest(request, cancellationToken);
    }
}
