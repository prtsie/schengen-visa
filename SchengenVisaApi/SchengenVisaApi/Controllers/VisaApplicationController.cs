using System.Security.Claims;
using ApplicationLayer.Services.VisaApplications.Handlers;
using ApplicationLayer.Services.VisaApplications.Models;
using ApplicationLayer.Services.VisaApplications.Requests;
using Domains.Users;
using Domains.VisaApplicationDomain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchengenVisaApi.Common;

namespace SchengenVisaApi.Controllers;

/// Controller for <see cref="Domains.VisaApplicationDomain"/>
[ApiController]
[Route("[controller]")]
public class VisaApplicationController(IVisaApplicationRequestsHandler visaApplicationRequestsHandler) : ControllerBase
{
    //todo should return only pending applications
    /// Returns all <see cref="Domains.VisaApplicationDomain.VisaApplication"/> from DB
    /// <remarks>Accessible only for <see cref="Role.ApprovingAuthority"/></remarks>
    [HttpGet]
    [ProducesResponseType<List<VisaApplication>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize(policy: PolicyConstants.ApprovingAuthorityPolicy)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var result = await visaApplicationRequestsHandler.Get(cancellationToken);
        return Ok(result);
    }

    /// Returns all <see cref="VisaApplication"/> of one applicant
    /// <remarks>Returns applications for authorized applicant</remarks>
    [HttpGet]
    [ProducesResponseType<List<VisaApplicationModelForApplicant>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(policy: PolicyConstants.ApplicantPolicy)]
    [Route("OfApplicant")]
    public async Task<IActionResult> GetForApplicant(CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        var result = await visaApplicationRequestsHandler.GetForApplicant(userId, cancellationToken);
        return Ok(result);
    }

    /// Adds new <see cref="VisaApplication"/> to DB
    /// <remarks>Adds application for authorized applicant</remarks>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(policy: PolicyConstants.ApplicantPolicy)]
    public async Task<IActionResult> Create(VisaApplicationCreateRequest request, CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        await visaApplicationRequestsHandler.HandleCreateRequest(userId, request, cancellationToken);
        return Created();
    }

    private Guid GetUserId() => Guid.Parse(HttpContext.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
}
