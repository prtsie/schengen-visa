using ApplicationLayer.Services.VisaApplications.Handlers;
using ApplicationLayer.Services.VisaApplications.Models;
using ApplicationLayer.Services.VisaApplications.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchengenVisaApi.Common;

namespace SchengenVisaApi.Controllers;

/// <summary> Controller for <see cref="Domains.VisaApplicationDomain"/> </summary>
[ApiController]
[Route("visaApplications")]
public class VisaApplicationController(IVisaApplicationRequestsHandler visaApplicationRequestsHandler) : VisaApiControllerBase
{
    /// <summary> Returns all applications from DB </summary>
    /// <remarks> Accessible only for approving authorities </remarks>
    [HttpGet]
    [ProducesResponseType<List<VisaApplicationModelForAuthority>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize(policy: PolicyConstants.ApprovingAuthorityPolicy)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var result = await visaApplicationRequestsHandler.GetAllAsync(cancellationToken);
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
        var result = await visaApplicationRequestsHandler.GetForApplicantAsync(userId, cancellationToken);
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
        await visaApplicationRequestsHandler.HandleCreateRequestAsync(userId, request, cancellationToken);
        return Ok();
    }

    /// <summary> Sets application status to closed</summary>
    /// <remarks> Accessible only for applicant</remarks>
    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(policy: PolicyConstants.ApplicantPolicy)]
    [Route("{applicationId:guid}")]
    public async Task<IActionResult> CloseApplication(Guid applicationId, CancellationToken cancellationToken)
    {
        var userId = GetUserId();
        await visaApplicationRequestsHandler.HandleCloseRequestAsync(userId, applicationId, cancellationToken);
        return Ok();
    }

    /// <summary> Allows approving authorities approve or reject applications</summary>
    /// <remarks> Accessible only for authorities</remarks>
    [HttpPatch]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(policy: PolicyConstants.ApprovingAuthorityPolicy)]
    [Route("approving/{applicationId:guid}")]
    public async Task<IActionResult> SetStatusFromAuthority(Guid applicationId, AuthorityRequestStatuses status, CancellationToken cancellationToken)
    {
        await visaApplicationRequestsHandler.SetApplicationStatusFromAuthorityAsync(applicationId, status, cancellationToken);
        return Ok();
    }
}
