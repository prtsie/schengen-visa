using ApplicationLayer.Services.Locations.Requests;

namespace ApplicationLayer.Services.Locations.RequestHandlers.AdminRequests
{
    /// Handles edit requests of locations from admins
    public interface IEditLocationsRequestsHandler
    {
        /// Handles add country requests
        Task AddCountryAsync(AddCountryRequest request, CancellationToken cancellationToken);
    }
}
