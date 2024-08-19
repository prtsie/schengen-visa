using System.Security.Claims;
using ApplicationLayer.Services.VisaApplications.Handlers;
using ApplicationLayer.Services.VisaApplications.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchengenVisaApi.Common;

namespace SchengenVisaApi.Controllers;

[ApiController]
[Route("[controller]")]
public class VisaApplicationController(IVisaApplicationRequestsHandler visaApplicationRequestsHandler) : ControllerBase
{
    [HttpGet]
    [Authorize(policy: PolicyConstants.ApprovingAuthorityPolicy)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var result = await visaApplicationRequestsHandler.Get(cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    [Authorize(policy: PolicyConstants.ApplicantPolicy)]
    public void Create(VisaApplicationCreateRequest request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
        visaApplicationRequestsHandler.HandleCreateRequest(userId, request, cancellationToken);
    }
}
