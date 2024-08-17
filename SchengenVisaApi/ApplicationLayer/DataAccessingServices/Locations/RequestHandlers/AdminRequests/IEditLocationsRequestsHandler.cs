using ApplicationLayer.DataAccessingServices.Locations.Requests;

namespace ApplicationLayer.DataAccessingServices.Locations.RequestHandlers.AdminRequests
{
    /// Handles edit requests of locations from admins
    public interface IEditLocationsRequestsHandler
    {
        /// Handles add country requests
        Task AddCountryAsync(AddCountryRequest request, CancellationToken cancellationToken);
    }
}
