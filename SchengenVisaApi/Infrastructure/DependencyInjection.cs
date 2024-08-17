using ApplicationLayer.AuthServices.NeededServices;
using ApplicationLayer.DataAccessingServices.Applicants.NeededServices;
using ApplicationLayer.DataAccessingServices.Locations.NeededServices;
using ApplicationLayer.DataAccessingServices.VisaApplications.NeededServices;
using ApplicationLayer.GeneralNeededServices;
using Infrastructure.Auth;
using Infrastructure.Common;
using Infrastructure.Database;
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
