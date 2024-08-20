using ApplicationLayer.Services.AuthServices.LoginService;
using ApplicationLayer.Services.AuthServices.RegisterService;
using ApplicationLayer.Services.AuthServices.Requests;
using Domains.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchengenVisaApi.Common;

namespace SchengenVisaApi.Controllers
{
    /// Controller for <see cref="Domains.Users"/>
    [ApiController]
    [Route("auth")]
    public class UsersController(IRegisterService registerService, ILoginService loginService) : ControllerBase
    {
        /// Adds applicant with user account to DB
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [Route("applicant")]
        public async Task<IActionResult> Register(RegisterApplicantRequest request, CancellationToken cancellationToken)
        {
            await registerService.Register(request, cancellationToken);
            return Created();
        }

        /// Adds approving authority with user account to DB
        /// <remarks>Accessible only for <see cref="Role.Admin"/></remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Route("authority")]
        [Authorize(policy: PolicyConstants.AdminPolicy)]
        public async Task<IActionResult> RegisterAuthority(RegisterRequest request, CancellationToken cancellationToken)
        {
            await registerService.RegisterAuthority(request, cancellationToken);
            return Created();
        }

        /// Returns JWT-token for authentication
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
