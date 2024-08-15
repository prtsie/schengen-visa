using ApplicationLayer.Applicants;
using ApplicationLayer.Common;
using ApplicationLayer.Locations;
using ApplicationLayer.VisaApplications;
using Infrastructure.Database;
using Infrastructure.Database.Applicants.Repositories;
using Infrastructure.Database.Generic;
using Infrastructure.Database.Locations.Repositories.Cities;
using Infrastructure.Database.Locations.Repositories.Countries;
using Infrastructure.Database.VisaApplications.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using DbContext = Infrastructure.Database.DbContext;

namespace Infrastructure;

/// Provides methods to add services to DI-container
public static class DependencyInjection
{
    /// Add services needed for Infrastructure layer
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        //TODO строка подключения
        services.AddDbContext<DbContext>(opts =>
            opts.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=visadb;Integrated Security=True;"));

        services.AddScoped<IGenericReader>(serviceProvider => serviceProvider.GetRequiredService<DbContext>());
        services.AddScoped<IGenericWriter>(serviceProvider => serviceProvider.GetRequiredService<DbContext>());
        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<DbContext>());

        services.AddScoped<IApplicantsRepository, ApplicantsRepository>();
        services.AddScoped<IVisaApplicationsRepository, VisaApplicationsRepository>();
        services.AddScoped<ICitiesRepository, CitiesRepository>();
        services.AddScoped<ICountriesRepository, CountriesRepository>();

        return services;
    }
}