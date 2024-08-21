using ApplicationLayer.Services.AuthServices.LoginService;
using ApplicationLayer.Services.AuthServices.RegisterService;
using ApplicationLayer.Services.AuthServices.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchengenVisaApi.Common;

namespace SchengenVisaApi.Controllers
{
    ///<summary> Controller for user-auth and registration </summary>
    [ApiController]
    [Route("auth")]
    public class UsersController(IRegisterService registerService, ILoginService loginService) : ControllerBase
    {
        /// <summary> Adds applicant with user account to DB </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [Route("applicant")]
        public async Task<IActionResult> Register(RegisterApplicantRequest request, CancellationToken cancellationToken)
        {
            await registerService.Register(request, cancellationToken);
            return Ok();
        }

        /// <summary> Adds approving authority with user account to DB </summary>
        ///<remarks> Accessible only for admins </remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Route("authority")]
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
        public async Task<IActionResult> Login(string email, string password, CancellationToken cancellationToken)
        {
            var result = await loginService.LoginAsync(new UserLoginRequest(email, password), cancellationToken);
            return Ok(result);
        }
    }
}
