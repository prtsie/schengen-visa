using ApplicationLayer.Services.Applicants.Models;
using ApplicationLayer.Services.AuthServices.Common;
using ApplicationLayer.Services.AuthServices.LoginService;
using ApplicationLayer.Services.AuthServices.RegisterService;
using ApplicationLayer.Services.AuthServices.Requests;
using ApplicationLayer.Services.Users;
using ApplicationLayer.Services.Users.Models;
using ApplicationLayer.Services.Users.Requests;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchengenVisaApi.Common;

namespace SchengenVisaApi.Controllers;

///<summary> Controller for user-auth and registration </summary>
[ApiController]
[Route("users")]
public class UsersController(
    IRegisterService registerService,
    ILoginService loginService,
    IUsersService usersService,
    IValidator<RegisterApplicantRequest> registerApplicantRequestValidator,
    IValidator<ChangeUserAuthDataRequest> changeUserAuthDataRequestValidator,
    IValidator<RegisterRequest> registerRequestValidator) : ControllerBase
{
    /// <summary> Adds applicant with user account </summary>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(RegisterApplicantRequest request, CancellationToken cancellationToken)
    {
        await registerApplicantRequestValidator.ValidateAndThrowAsync(request, cancellationToken);

        await registerService.RegisterApplicant(request, cancellationToken);
        return Ok();
    }

    /// <summary> Adds approving authority with user account </summary>
    ///<remarks> Accessible only for admins </remarks>
    [HttpPost("authorities")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(policy: PolicyConstants.AdminPolicy)]
    public async Task<IActionResult> RegisterAuthority(RegisterRequest request, CancellationToken cancellationToken)
    {
        await registerRequestValidator.ValidateAndThrowAsync(request, cancellationToken);

        await registerService.RegisterAuthority(request, cancellationToken);
        return Ok();
    }

    /// <summary> Returns JWT-token for authentication </summary>
    [HttpGet("login")]
    [ProducesResponseType<AuthToken>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Login(string email, string password, CancellationToken cancellationToken)
    {
        var loginRequest = new LoginRequest
        {
            AuthData = new() { Email = email, Password = password }
        };

        var result = await loginService.LoginAsync(loginRequest, cancellationToken);
        return Ok(result);
    }

    /// <summary> Returns list of authority accounts </summary>
    /// <remarks> Accessible only for admins </remarks>
    [HttpGet("authorities")]
    [ProducesResponseType<List<UserModel>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize(policy: PolicyConstants.AdminPolicy)]
    public async Task<IActionResult> GetAuthorityAccounts(CancellationToken cancellationToken)
    {
        var result = await usersService.GetAuthoritiesAccountsAsync(cancellationToken);
        return Ok(result);
    }

    /// <summary> Changes authority's account authentication data </summary>
    /// <remarks> Accessible only for admins </remarks>
    [HttpPut("authorities")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(policy: PolicyConstants.AdminPolicy)]
    public async Task<IActionResult> ChangeAuthorityAuthData(ChangeUserAuthDataRequest request, CancellationToken cancellationToken)
    {
        await changeUserAuthDataRequestValidator.ValidateAndThrowAsync(request, cancellationToken);

        await usersService.ChangeAuthorityAuthDataAsync(request, cancellationToken);
        return Ok();
    }

    /// <summary> Removes authority's account </summary>
    /// <remarks> Accessible only for admins </remarks>
    [HttpDelete("authorities/{authorityAccountId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Authorize(policy: PolicyConstants.AdminPolicy)]
    public async Task<IActionResult> RemoveAuthorityAccount(Guid authorityAccountId, CancellationToken cancellationToken)
    {
        await usersService.RemoveAuthorityAccount(authorityAccountId, cancellationToken);
        return Ok();
    }

    /// <summary> Returns applicant info </summary>
    [HttpGet("applicant")]
    [ProducesResponseType<ApplicantModel>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [Authorize(policy: PolicyConstants.ApplicantPolicy)]
    public async Task<IActionResult> GetApplicant(CancellationToken cancellationToken)
    {

        var result = await usersService.GetAuthenticatedApplicant(cancellationToken);
        return Ok(result);
    }
}
