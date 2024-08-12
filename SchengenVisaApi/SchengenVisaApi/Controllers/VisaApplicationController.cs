using Microsoft.AspNetCore.Mvc;

namespace SchengenVisaApi.Controllers;

[ApiController]
[Route("[controller]")]
public class VisaApplicationController : ControllerBase
{

    public VisaApplicationController()
    {

    }

    [HttpGet]
    public void Create()
    {
        throw new NotImplementedException();
    }
}
