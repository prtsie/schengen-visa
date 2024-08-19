using ApplicationLayer.Services.AuthServices.LoginService;
using ApplicationLayer.Services.AuthServices.RegisterService;
using ApplicationLayer.Services.AuthServices.Requests;
using Microsoft.AspNetCore.Mvc;

namespace SchengenVisaApi.Controllers
{
    [ApiController]
    [Route("auth")]
    public class UsersController(IRegisterService registerService, ILoginService loginService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Register(RegisterApplicantRequest request, CancellationToken cancellationToken)
        {
            await registerService.Register(request, cancellationToken);
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
