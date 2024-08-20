using ApplicationLayer.Services.Locations.RequestHandlers;
using ApplicationLayer.Services.Locations.Requests;
using Domains.LocationDomain;
using Domains.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchengenVisaApi.Common;

namespace SchengenVisaApi.Controllers
{
    /// Controller for <see cref="Domains.LocationDomain"/>
    [ApiController]
    [Route("locations")]
    public class LocationsController(ILocationRequestsHandler requestsHandler) : ControllerBase
    {
        /// Return countries with cities from DB
        [HttpGet]
        [ProducesResponseType<List<Country>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAvailableLocations(CancellationToken cancellationToken)
        {
            return Ok(await requestsHandler.HandleGetRequestAsync(cancellationToken));
        }

        /// Adds country with cities to DB
        /// <remarks>Accessible only for <see cref="Role.Admin"/></remarks>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Route("country")]
        [Authorize(policy: PolicyConstants.AdminPolicy)]
        public async Task<IActionResult> AddCountry(AddCountryRequest request, CancellationToken cancellationToken)
        {
            await requestsHandler.AddCountryAsync(request, cancellationToken);
            return Ok();
        }

        /// Updates country with cities in DB
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Route("country")]
        [Authorize(policy: PolicyConstants.AdminPolicy)]
        public async Task<IActionResult> UpdateCountry(UpdateCountryRequest request, CancellationToken cancellationToken)
        {
            await requestsHandler.UpdateCountryAsync(request, cancellationToken);
            return Ok();
        }
    }
}
