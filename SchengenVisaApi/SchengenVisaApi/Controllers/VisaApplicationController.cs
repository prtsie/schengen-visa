using ApplicationLayer.Services.VisaApplications.Handlers;
using ApplicationLayer.Services.VisaApplications.Models;
using ApplicationLayer.Services.VisaApplications.Requests;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchengenVisaApi.Common;

namespace SchengenVisaApi.Controllers;

/// <summary> Controller for <see cref="Domains.VisaApplicationDomain"/> </summary>
[ApiController]
[Route("visaApplications")]
public class VisaApplicationController(
    IVisaApplicationRequestsHandler visaApplicationRequestsHandler,
    IValidator<VisaApplicationCreateRequest> visaApplicationCreateRequestValidator) : ControllerBase
{
    /// <summary> Returns pending applications from DB </summary>
    /// <remarks> Accessible only for approving authorities </remarks>
    [HttpGet("pending")]
    [ProducesResponseType<List<VisaApplicationModelForAuthority>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize(policy: PolicyConstants.ApprovingAuthorityPolicy)]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var result = await visaApplicationRequestsHandler.GetPendingAsync(cancellationToken);
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
    [Route("ofApplicant")]
    public async Task<IActionResult> GetForApplicant(CancellationToken cancellationToken)
    {
        var result = await visaApplicationRequestsHandler.GetForApplicantAsync(cancellationToken);
        return Ok(result);
    }

    /// <summary> Adds new application to DB </summary>
    /// <remarks> Adds application for authorized applicant </remarks>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(policy: PolicyConstants.ApplicantPolicy)]
    public async Task<IActionResult> Create(VisaApplicationCreateRequest request, CancellationToken cancellationToken)
    {
        await visaApplicationCreateRequestValidator.ValidateAndThrowAsync(request, cancellationToken);

        await visaApplicationRequestsHandler.HandleCreateRequestAsync(request, cancellationToken);
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
        await visaApplicationRequestsHandler.HandleCloseRequestAsync(applicationId, cancellationToken);
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
