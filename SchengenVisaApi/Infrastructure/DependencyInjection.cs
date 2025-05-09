using System.Reflection;
using ApplicationLayer.InfrastructureServicesInterfaces;
using ApplicationLayer.Services.Applicants.NeededServices;
using ApplicationLayer.Services.AuthServices.NeededServices;
using ApplicationLayer.Services.VisaApplications.NeededServices;
using Infrastructure.Common;
using Infrastructure.Database;
using Infrastructure.Database.Applicants.Repositories;
using Infrastructure.Database.Generic;
using Infrastructure.Database.Users.Repositories;
using Infrastructure.Database.VisaApplications.Repositories;
using Infrastructure.EntityToExcelTemplateWriter;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

/// Provides methods to add services to DI-container
public static class DependencyInjection
{
    /// Add services needed for Infrastructure layer
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfigurationManager configurationManager)
    {

        services.AddDbContext<DatabaseContext>(opts =>
            opts.UseNpgsql(configurationManager.GetConnectionString("connectionString")));

        services.AddScoped<IGenericReader>(serviceProvider => serviceProvider.GetRequiredService<DatabaseContext>());
        services.AddScoped<IGenericWriter>(serviceProvider => serviceProvider.GetRequiredService<DatabaseContext>());
        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<DatabaseContext>());

        services.AddScoped<IApplicantsRepository, ApplicantsRepository>();
        services.AddScoped<IVisaApplicationsRepository, VisaApplicationsRepository>();
        services.AddScoped<IUsersRepository, UsersRepository>();

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<IEntityWriter, ExcelWriter>();

        services.AddHttpContextAccessor();
        services.AddScoped<IUserIdProvider, UserIdProvider>();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }
}
