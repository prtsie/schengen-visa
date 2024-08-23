using ApplicationLayer.Services.ApprovingAuthorities;
using ApplicationLayer.Services.AuthServices.LoginService;
using ApplicationLayer.Services.AuthServices.RegisterService;
using ApplicationLayer.Services.AuthServices.Requests;
using Domains.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchengenVisaApi.Common;

namespace SchengenVisaApi.Controllers
{
    ///<summary> Controller for user-auth and registration </summary>
    [ApiController]
    [Route("users")]
    public class UsersController(
        IRegisterService registerService,
        ILoginService loginService,
        IUsersService authorityService) : VisaApiControllerBase
    {
        /// <summary> Adds applicant with user account to DB </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterApplicantRequest request, CancellationToken cancellationToken)
        {
            await registerService.RegisterApplicant(request, cancellationToken);
            return Ok();
        }

        /// <summary> Adds approving authority with user account to DB </summary>
        ///<remarks> Accessible only for admins </remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Route("authorities")]
        [Authorize(policy: PolicyConstants.AdminPolicy)]
        public async Task<IActionResult> RegisterAuthority(RegisterRequest request, CancellationToken cancellationToken)
        {
            await registerService.RegisterAuthority(request, cancellationToken);
            return Ok();
        }

        /// <summary> Returns JWT-token for authentication </summary>
        [HttpGet]
        [ProducesResponseType<string>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [Route("login")]
        public async Task<IActionResult> Login(string email, string password, CancellationToken cancellationToken)
        {
            var result = await loginService.LoginAsync(new UserLoginRequest(email, password), cancellationToken);
            return Ok(result);
        }

        /// <summary> Returns list of authority accounts </summary>
        /// <remarks> Accessible only for admins </remarks>
        [HttpGet]
        [ProducesResponseType<List<User>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Route("authorities")]
        [Authorize(policy: PolicyConstants.AdminPolicy)]
        //todo return models
        public async Task<IActionResult> GetAuthorityAccounts(CancellationToken cancellationToken)
        {
            var result = await authorityService.GetAuthoritiesAccountsAsync(cancellationToken);
            return Ok(result);
        }

        /// <summary> Changes authority's account authentication data </summary>
        /// <remarks> Accessible only for admins </remarks>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Route("authorities/{authorityAccountId:guid}")]
        [Authorize(policy: PolicyConstants.AdminPolicy)]
        //todo replace args with ChangeAuthorityAuthDataRequest or something
        public async Task<IActionResult> ChangeAuthorityAuthData(Guid authorityAccountId, RegisterRequest authData, CancellationToken cancellationToken)
        {
            await authorityService.ChangeAccountAuthDataAsync(authorityAccountId, authData, cancellationToken);
            return Ok();
        }

        /// <summary> Removes authority's account authentication data </summary>
        /// <remarks> Accessible only for admins </remarks>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Route("authorities/{authorityAccountId:guid}")]
        [Authorize(policy: PolicyConstants.AdminPolicy)]
        public async Task<IActionResult> RemoveAuthorityAccount(Guid authorityAccountId, CancellationToken cancellationToken)
        {
            await authorityService.RemoveUserAccount(authorityAccountId, cancellationToken);
            return Ok();
        }
    }
}
