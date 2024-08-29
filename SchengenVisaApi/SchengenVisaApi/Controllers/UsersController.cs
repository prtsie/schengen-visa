using ApplicationLayer.Services.AuthServices.Common;
using ApplicationLayer.Services.AuthServices.LoginService;
using ApplicationLayer.Services.AuthServices.RegisterService;
using ApplicationLayer.Services.AuthServices.Requests;
using ApplicationLayer.Services.Users;
using ApplicationLayer.Services.Users.Requests;
using Domains.Users;
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
    IValidator<AuthData> authDataValidator,
    IValidator<RegisterRequest> registerRequestValidator) : ControllerBase
{
    /// <summary> Adds applicant with user account </summary>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
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
    [ProducesResponseType(StatusCodes.Status409Conflict)]
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
    [ProducesResponseType<string>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> Login([FromQuery] LoginRequest request, CancellationToken cancellationToken)
    {
        var result = await loginService.LoginAsync(request, cancellationToken);
        return Ok(result);
    }

    /// <summary> Returns list of authority accounts </summary>
    /// <remarks> Accessible only for admins </remarks>
    [HttpGet("authorities")]
    [ProducesResponseType<List<User>>(StatusCodes.Status200OK)]
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
    [HttpPut("authorities/{authorityAccountId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(policy: PolicyConstants.AdminPolicy)]
    public async Task<IActionResult> ChangeAuthorityAuthData(Guid authorityAccountId, AuthData authData, CancellationToken cancellationToken)
    {
        await authDataValidator.ValidateAndThrowAsync(authData, cancellationToken);

        await usersService.ChangeAuthorityAuthDataAsync(new ChangeUserAuthDataRequest(authorityAccountId, authData), cancellationToken);
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
}
