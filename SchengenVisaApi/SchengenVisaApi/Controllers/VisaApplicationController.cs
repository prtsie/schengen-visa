using System.Security.Claims;
using ApplicationLayer.Services.VisaApplications.Handlers;
using ApplicationLayer.Services.VisaApplications.Models;
using ApplicationLayer.Services.VisaApplications.Requests;
using Domains.VisaApplicationDomain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchengenVisaApi.Common;

namespace SchengenVisaApi.Controllers;

/// <summary> Controller for <see cref="Domains.VisaApplicationDomain"/> </summary>
[ApiController]
[Route("visaApplication")]
public class VisaApplicationController(IVisaApplicationRequestsHandler visaApplicationRequestsHandler) : VisaApiControllerBase
{
    //todo should return only pending applications
    //todo should return model
    /// <summary> Returns all applications from DB </summary>
    /// <remarks> Accessible only for approving authorities </remarks>
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

    /// <summary> Returns all applications of one applicant </summary>
    /// <remarks> Returns applications of authorized applicant </remarks>
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

    /// <summary> Adds new application to DB </summary>
    /// <remarks> Adds application for authorized applicant </remarks>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(policy: PolicyConstants.ApplicantPolicy)]
    public async Task<IActionResult> Create(VisaApplicationCreateRequest request, CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        await visaApplicationRequestsHandler.HandleCreateRequest(userId, request, cancellationToken);
        return Ok();
    }
}
