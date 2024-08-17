using System.Reflection;
using System.Text;
using ApplicationLayer;
using Infrastructure;
using Infrastructure.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace SchengenVisaApi;

/// Provides methods to add services to DI-container
public static class DependencyInjection
{
    /// Add needed services
    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        var config = builder.Configuration;
        var environment = builder.Environment;

        builder.Services
            .AddInfrastructure(config, environment.IsDevelopment())
            .AddApplicationLayer()
            .AddAuth(config)
            .AddPresentation(environment);
    }

    /// Add services needed for Presentation layer
    private static void AddPresentation(this IServiceCollection services,
        IWebHostEnvironment environment)
    {
        if (environment.IsDevelopment())
        {
            services.AddSwagger();
        }

        services.AddControllers();
    }

    /// Adds authentication, authorization and token generator
    private static IServiceCollection AddAuth(this IServiceCollection services, IConfigurationManager configurationManager)
    {
        var parameters = new TokenValidationParameters
        {
            ValidIssuer = configurationManager["JwtSettings:Issuer"],
            ValidAudience = configurationManager["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configurationManager["JwtSettings:Key"]!)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
        };

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(opts => opts.TokenValidationParameters = parameters);
        services.AddAuthorization();

        services.AddTokenGenerator(new TokenGeneratorOptions(
            Issuer: parameters.ValidIssuer!,
            Audience: parameters.ValidAudience!,
            Credentials: new SigningCredentials(parameters.IssuerSigningKey, SecurityAlgorithms.HmacSha256),
            ValidTime: TimeSpan.FromMinutes(30)
        ));

        return services;
    }

    /// Add swagger
    private static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
    }
}
