﻿using System.IdentityModel.Tokens.Jwt;
using ApplicationLayer.DataAccessingServices.AuthServices.NeededServices;
using ApplicationLayer.GeneralNeededServices;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Auth
{
    public static class ServiceCollectionsExtensions
    {
        public static IServiceCollection AddTokenGenerator(this IServiceCollection services, TokenGeneratorOptions options)
        {
            services.AddSingleton<JwtSecurityTokenHandler>();
            services.AddSingleton<ITokenGenerator, TokenGenerator>(provider =>
            {
                var tokenHandler = provider.GetRequiredService<JwtSecurityTokenHandler>();
                var dateTimeProvider = provider.GetRequiredService<IDateTimeProvider>();

                return new TokenGenerator(options, tokenHandler, dateTimeProvider);
            });

            return services;
        }
    }
}
