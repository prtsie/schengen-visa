using ApplicationLayer.Services.VisaApplications.Handlers;
using ApplicationLayer.Services.VisaApplications.Models;
using ApplicationLayer.Services.VisaApplications.Requests;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchengenVisaApi.Common;

namespace SchengenVisaApi.Controllers;

/// <summary> Controller for visa applications </summary>
[ApiController]
[Route("visaApplications")]
public class VisaApplicationController(
    IVisaApplicationRequestsHandler visaApplicationRequestsHandler,
    IValidator<VisaApplicationCreateRequest> visaApplicationCreateRequestValidator) : ControllerBase
{
    /// <summary> Returns pending applications </summary>
    /// <remarks> Accessible only for approving authorities </remarks>
    [HttpGet("pending")]
    [ProducesResponseType<List<VisaApplicationPreview>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize(policy: PolicyConstants.ApprovingAuthorityPolicy)]
    public async Task<IActionResult> GetPending(CancellationToken cancellationToken)
    {
        var result = await visaApplicationRequestsHandler.GetPendingAsync(cancellationToken);
        return Ok(result);
    }

    /// <summary> Returns application </summary>
    /// <remarks> Accessible only for approving authorities </remarks>
    [HttpGet("/forAuthority/{applicationId:guid}")]
    [ProducesResponseType<VisaApplicationModel>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [Authorize(policy: PolicyConstants.ApprovingAuthorityPolicy)]
    public async Task<IActionResult> GetApplicationForAuthority(Guid applicationId, CancellationToken cancellationToken)
    {
        var result = await visaApplicationRequestsHandler.GetApplicationForAuthorityAsync(applicationId, cancellationToken);
        return Ok(result);
    }

    /// <summary> Returns application </summary>
    /// <remarks> Accessible only for applicant </remarks>
    [HttpGet("/forApplicant/{applicationId:guid}")]
    [ProducesResponseType<VisaApplicationModel>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(policy: PolicyConstants.ApplicantPolicy)]
    public async Task<IActionResult> GetApplicationForApplicant(Guid applicationId, CancellationToken cancellationToken)
    {
        var result = await visaApplicationRequestsHandler.GetApplicationForApplicantAsync(applicationId, cancellationToken);
        return Ok(result);
    }

    /// <summary> Returns all applications of one applicant </summary>
    /// <remarks> Returns applications of authorized applicant </remarks>
    [HttpGet("ofApplicant")]
    [ProducesResponseType<List<VisaApplicationPreview>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(policy: PolicyConstants.ApplicantPolicy)]
    public async Task<IActionResult> GetApplicationsForApplicant(CancellationToken cancellationToken)
    {
        var result = await visaApplicationRequestsHandler.GetForApplicantAsync(cancellationToken);
        return Ok(result);
    }

    /// <summary> Adds new application </summary>
    /// <remarks> Adds application for authorized applicant </remarks>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(policy: PolicyConstants.ApplicantPolicy)]
    public async Task<IActionResult> CreateApplication(VisaApplicationCreateRequest request, CancellationToken cancellationToken)
    {
        await visaApplicationCreateRequestValidator.ValidateAndThrowAsync(request, cancellationToken);

        await visaApplicationRequestsHandler.HandleCreateRequestAsync(request, cancellationToken);
        return Ok();
    }

    /// <summary> Sets application status to closed</summary>
    /// <remarks> Accessible only for applicant</remarks>
    [HttpPatch("{applicationId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(policy: PolicyConstants.ApplicantPolicy)]
    public async Task<IActionResult> CloseApplication(Guid applicationId, CancellationToken cancellationToken)
    {
        await visaApplicationRequestsHandler.HandleCloseRequestAsync(applicationId, cancellationToken);
        return Ok();
    }

    /// <summary> Approve or reject applications</summary>
    /// <remarks> Accessible only for authorities</remarks>
    [HttpPatch("approving/{applicationId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [Authorize(policy: PolicyConstants.ApprovingAuthorityPolicy)]
    public async Task<IActionResult> SetStatusFromAuthority(Guid applicationId,
        AuthorityRequestStatuses status,
        CancellationToken cancellationToken)
    {
        await visaApplicationRequestsHandler.SetApplicationStatusFromAuthorityAsync(applicationId, status, cancellationToken);
        return Ok();
    }

    /// <summary> Returns application </summary>
    /// <remarks> Accessible only for applicant </remarks>
    [HttpGet("/forApplicant/{applicationId:guid}/download")]
    [Produces("application/octet-stream")]
    [ProducesResponseType<object>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Authorize(policy: PolicyConstants.ApplicantPolicy)]
    public async Task<IActionResult> DownloadApplicationForApplicant(Guid applicationId, CancellationToken cancellationToken)
    {
        var result = await visaApplicationRequestsHandler.ApplicationToStreamAsync(applicationId, cancellationToken);
        result.Position = 0;
        return File(result, "application/octet-stream", "Application.xlsx");
    }
}
