using ApplicationLayer.Services.Locations.RequestHandlers;
using ApplicationLayer.Services.Locations.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchengenVisaApi.Common;

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

        [HttpPost]
        [Route("country")]
        [Authorize(policy: PolicyConstants.AdminPolicy)]
        public async Task<IActionResult> AddCountry(AddCountryRequest request, CancellationToken cancellationToken)
        {
            await requestsHandler.AddCountryAsync(request, cancellationToken);
            return Ok();
        }
    }
}
