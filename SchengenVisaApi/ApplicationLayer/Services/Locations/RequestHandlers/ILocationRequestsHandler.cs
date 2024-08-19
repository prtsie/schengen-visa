using ApplicationLayer.Services.Locations.Requests;
using Domains.LocationDomain;

namespace ApplicationLayer.Services.Locations.RequestHandlers
{
    /// Handles location requests
    public interface ILocationRequestsHandler
    {
        /// Handle get request
        /// <returns>List of available countries</returns>
        Task<List<Country>> HandleGetRequestAsync(CancellationToken cancellationToken);

        /// Handles add country requests
        Task AddCountryAsync(AddCountryRequest request, CancellationToken cancellationToken);
    }
}
