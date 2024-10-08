﻿using System.Reflection;
using ApplicationLayer.Services.AuthServices.LoginService;
using ApplicationLayer.Services.AuthServices.RegisterService;
using ApplicationLayer.Services.Users;
using ApplicationLayer.Services.VisaApplications.Handlers;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationLayer;

/// Provides methods to add services to DI-container
public static class DependencyInjection
{
    /// Add services for Application layer
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services, bool isDevelopment = false)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddScoped<IVisaApplicationRequestsHandler, VisaApplicationRequestsHandler>();

        services.AddScoped<IRegisterService, RegisterService>();
        services.AddScoped<IUsersService, UsersService>();

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
