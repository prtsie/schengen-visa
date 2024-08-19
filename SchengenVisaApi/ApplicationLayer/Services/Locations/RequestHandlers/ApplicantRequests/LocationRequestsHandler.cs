using ApplicationLayer.Services.Locations.NeededServices;
using Domains.LocationDomain;

namespace ApplicationLayer.Services.Locations.RequestHandlers.ApplicantRequests
{
    /// <inheritdoc cref="ILocationRequestsHandler"/>
    public class LocationRequestsHandler(ICountriesRepository countries) : ILocationRequestsHandler
    {
        async Task<List<Country>> ILocationRequestsHandler.HandleGetRequestAsync(CancellationToken cancellationToken)
        {
            return await countries.GetAllAsync(cancellationToken);
        }
    }
}
