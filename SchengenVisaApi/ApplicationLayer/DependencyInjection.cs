using ApplicationLayer.VisaApplications.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationLayer;

/// Provides methods to add services to DI-container
public static class DependencyInjection
{
    /// Add services for Application layer
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddScoped<IVisaApplicationsRequestHandler, VisaApplicationRequestsHandler>();

        return services;
    }
}
