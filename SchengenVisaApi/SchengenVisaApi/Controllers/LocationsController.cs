using ApplicationLayer.DataAccessingServices.Locations.RequestHandlers.ApplicantRequests;
using Microsoft.AspNetCore.Mvc;

namespace SchengenVisaApi.Controllers
{
    [ApiController]
    [Route("countries")]
    public class LocationsController(ILocationRequestsHandler requestsHandler) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAvailableLocations(CancellationToken cancellationToken)
        {
            return Ok(await requestsHandler.HandleGetRequestAsync(cancellationToken));
        }
    }
}
