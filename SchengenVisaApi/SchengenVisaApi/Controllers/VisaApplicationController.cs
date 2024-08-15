using ApplicationLayer.VisaApplications.Requests;
using Microsoft.AspNetCore.Mvc;

namespace SchengenVisaApi.Controllers;

[ApiController]
[Route("[controller]")]
public class VisaApplicationController : ControllerBase
{

    public VisaApplicationController()
    {

        }

    [HttpPost]
    public void Create(CreateVisaApplicationRequest request)
    {
            throw new NotImplementedException();
        }
}
