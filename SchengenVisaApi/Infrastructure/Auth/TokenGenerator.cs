using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ApplicationLayer.InfrastructureServicesInterfaces;
using ApplicationLayer.Services.AuthServices.Common;
using ApplicationLayer.Services.AuthServices.NeededServices;
using Domains.Users;

namespace Infrastructure.Auth;

/// <inheritdoc cref="ITokenGenerator"/>
/// <param name="options">options kind of one in authorization registration in DI methods</param>
/// <param name="tokenHandler">token handler</param>
/// <param name="dateTimeProvider">date time provider</param>
public class TokenGenerator(TokenGeneratorOptions options, JwtSecurityTokenHandler tokenHandler, IDateTimeProvider dateTimeProvider)
    : ITokenGenerator
{
    /// <inheritdoc cref="ITokenGenerator.CreateToken"/>
    public AuthToken CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Role, user.Role.ToString()),
            new(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var token = new JwtSecurityToken(
            issuer: options.Issuer,
            audience: options.Audience,
            expires: dateTimeProvider.Now().Add(options.ValidTime),
            signingCredentials: options.Credentials,
            claims: claims);

        return new AuthToken(tokenHandler.WriteToken(token));
    }
}
