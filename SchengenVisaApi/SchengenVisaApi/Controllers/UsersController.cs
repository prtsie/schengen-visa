using ApplicationLayer.Services.AuthServices.LoginService;
using ApplicationLayer.Services.AuthServices.RegisterService;
using ApplicationLayer.Services.AuthServices.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchengenVisaApi.Common;

namespace SchengenVisaApi.Controllers
{
    [ApiController]
    [Route("auth")]
    public class UsersController(IRegisterService registerService, ILoginService loginService) : ControllerBase
    {
        [HttpPost]
        [Route("applicant")]
        public async Task<IActionResult> Register(RegisterApplicantRequest request, CancellationToken cancellationToken)
        {
            await registerService.Register(request, cancellationToken);
            return Created();
        }

        [HttpPost]
        [Authorize(policy: PolicyConstants.AdminPolicy)]
        [Route("authority")]
        public async Task<IActionResult> RegisterAuthority(RegisterRequest request, CancellationToken cancellationToken)
        {
            await registerService.RegisterAuthority(request, cancellationToken);
            return Created();
        }

        [HttpGet]
        public async Task<IActionResult> Login(string email, string password, CancellationToken cancellationToken)
        {
            var result = await loginService.LoginAsync(new UserLoginRequest(email, password), cancellationToken);
            return Ok(result);
        }
    }
}
