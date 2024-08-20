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

    [HttpGet]
    [Authorize(policy: PolicyConstants.ApplicantPolicy)]
    [Route("OfApplicant")]
    public async Task<IActionResult> GetForApplicant(CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        var result = await visaApplicationRequestsHandler.GetForApplicant(userId, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    [Authorize(policy: PolicyConstants.ApplicantPolicy)]
    public async Task<IActionResult> Create(VisaApplicationCreateRequest request, CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        await visaApplicationRequestsHandler.HandleCreateRequest(userId, request, cancellationToken);
        return Created();
    }

    private Guid GetUserId() => Guid.Parse(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
}
