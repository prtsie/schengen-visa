using ApplicationLayer.DataAccessingServices.Locations.RequestHandlers.AdminRequests;
using ApplicationLayer.DataAccessingServices.Locations.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchengenVisaApi.Common;

namespace SchengenVisaApi.Controllers
{
    [ApiController]
    [Route("admin")]
    [Authorize(policy: PolicyConstants.AdminPolicy)]
    public class AdminController(IEditLocationsRequestsHandler requestsHandler) : ControllerBase
    {
        [HttpPost]
        [Route("country")]
        public async Task<IActionResult> AddCountry(AddCountryRequest request, CancellationToken cancellationToken)
        {
            await requestsHandler.AddCountryAsync(request, cancellationToken);
            return Ok();
        }
    }
}
