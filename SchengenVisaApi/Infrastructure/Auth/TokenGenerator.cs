using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ApplicationLayer.InfrastructureServicesInterfaces;
using ApplicationLayer.Services.AuthServices.NeededServices;
using Domains.Users;

namespace Infrastructure.Auth;

public class TokenGenerator(TokenGeneratorOptions options, JwtSecurityTokenHandler tokenHandler, IDateTimeProvider dateTimeProvider)
    : ITokenGenerator
{
    public string CreateToken(User user)
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

        return tokenHandler.WriteToken(token);
    }
}