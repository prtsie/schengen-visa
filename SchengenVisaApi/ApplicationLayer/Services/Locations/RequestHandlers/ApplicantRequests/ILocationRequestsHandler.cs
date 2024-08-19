using Domains.LocationDomain;

namespace ApplicationLayer.Services.Locations.RequestHandlers.ApplicantRequests
{
    /// Handles location requests
    public interface ILocationRequestsHandler
    {
        /// Handle get request
        /// <returns>List of available countries</returns>
        Task<List<Country>> HandleGetRequestAsync(CancellationToken cancellationToken);
    }
}
