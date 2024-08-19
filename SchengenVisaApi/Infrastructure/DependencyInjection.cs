using ApplicationLayer.GeneralNeededServices;
using ApplicationLayer.Services.Applicants.NeededServices;
using ApplicationLayer.Services.AuthServices.NeededServices;
using ApplicationLayer.Services.Locations.NeededServices;
using ApplicationLayer.Services.VisaApplications.NeededServices;
using Infrastructure.Common;
using Infrastructure.Database.Applicants.Repositories;
using Infrastructure.Database.Generic;
using Infrastructure.Database.Locations.Repositories.Cities;
using Infrastructure.Database.Locations.Repositories.Countries;
using Infrastructure.Database.Users.Repositories;
using Infrastructure.Database.VisaApplications.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DbContext = Infrastructure.Database.DbContext;

namespace Infrastructure;

/// Provides methods to add services to DI-container
public static class DependencyInjection
{
    /// Add services needed for Infrastructure layer
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfigurationManager configurationManager,
        bool isDevelopment)
    {
        var databaseName = isDevelopment ? "developmentDB" : "normal'naya database";

        services.AddDbContextFactory<DbContext>(opts =>
            opts.UseSqlServer(configurationManager.GetConnectionString(databaseName)));

        services.AddScoped<IGenericReader>(serviceProvider => serviceProvider.GetRequiredService<DbContext>());
        services.AddScoped<IGenericWriter>(serviceProvider => serviceProvider.GetRequiredService<DbContext>());
        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<DbContext>());

        services.AddScoped<IApplicantsRepository, ApplicantsRepository>();
        services.AddScoped<IVisaApplicationsRepository, VisaApplicationsRepository>();
        services.AddScoped<ICitiesRepository, CitiesRepository>();
        services.AddScoped<ICountriesRepository, CountriesRepository>();
        services.AddScoped<IUsersRepository, UsersRepository>();

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}
