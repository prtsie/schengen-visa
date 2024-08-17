using ApplicationLayer.AuthServices.LoginService;
using ApplicationLayer.AuthServices.RegisterService;
using ApplicationLayer.DataAccessingServices.VisaApplications.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationLayer;

/// Provides methods to add services to DI-container
public static class DependencyInjection
{
    /// Add services for Application layer
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddScoped<IVisaApplicationsRequestHandler, VisaApplicationRequestsHandler>();

        services.AddScoped<IRegisterService, RegisterService>();
        services.AddScoped<ILoginService, LoginService>();

        return services;
    }
}
