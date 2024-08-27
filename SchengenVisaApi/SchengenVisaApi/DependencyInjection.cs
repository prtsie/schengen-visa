using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using ApplicationLayer;
using Domains.Users;
using Infrastructure;
using Infrastructure.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SchengenVisaApi.Common;
using SchengenVisaApi.ExceptionFilters;
using Swashbuckle.AspNetCore.SwaggerGen;

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
            .AddApplicationLayer(environment.IsDevelopment())
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

        services.AddProblemDetails();

        services.AddControllers(opts => opts.Filters.Add<GlobalExceptionsFilter>())
            .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
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
        services.AddAuthorizationBuilder().ConfigureAuthorizationPolicies();

        services.AddTokenGenerator(new TokenGeneratorOptions(
            Issuer: parameters.ValidIssuer!,
            Audience: parameters.ValidAudience!,
            Credentials: new SigningCredentials(parameters.IssuerSigningKey, SecurityAlgorithms.HmacSha256),
            ValidTime: TimeSpan.FromMinutes(30)
        ));

        return services;
    }

    /// Configure roles
    private static void ConfigureAuthorizationPolicies(this AuthorizationBuilder builder)
    {
        builder.AddPolicy(
                PolicyConstants.AdminPolicy,
                p => p.RequireClaim(ClaimTypes.Role, Role.Admin.ToString()))
            .AddPolicy(
                PolicyConstants.ApprovingAuthorityPolicy,
                p => p.RequireClaim(ClaimTypes.Role, Role.ApprovingAuthority.ToString()))
            .AddPolicy(
                PolicyConstants.ApplicantPolicy,
                p => p.RequireClaim(ClaimTypes.Role, Role.Applicant.ToString()));
    }

    /// Add swagger
    private static void AddSwagger(this IServiceCollection services)
    {
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        services.AddSwaggerGen(options =>
        {
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

            options.CustomOperationIds(apiDescription =>
                apiDescription.TryGetMethodInfo(out MethodInfo methodInfo) ? methodInfo.Name : null);
        });
    }
}
