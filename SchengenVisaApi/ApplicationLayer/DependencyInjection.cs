using ApplicationLayer.Services.AuthServices.LoginService;
using ApplicationLayer.Services.AuthServices.RegisterService;
using ApplicationLayer.Services.Locations.RequestHandlers;
using ApplicationLayer.Services.VisaApplications.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationLayer;

/// Provides methods to add services to DI-container
public static class DependencyInjection
{
    /// Add services for Application layer
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services, bool isDevelopment = false)
    {
        services.AddScoped<IVisaApplicationRequestsHandler, VisaApplicationRequestsHandler>();
        services.AddScoped<ILocationRequestsHandler, LocationRequestsHandler>();

        services.AddScoped<IRegisterService, RegisterService>();

        if (isDevelopment)
        {
            services.AddScoped<ILoginService, DevelopmentLoginService>();
        }
        else
        {
            services.AddScoped<ILoginService, LoginService>();
        }

        return services;
    }
}
