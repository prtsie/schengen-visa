using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ApplicationLayer.AuthServices.NeededServices;
using ApplicationLayer.GeneralNeededServices;
using Domains.Users;

namespace Infrastructure.Auth
{
    public class TokenGenerator(TokenGeneratorOptions options, JwtSecurityTokenHandler tokenHandler, IDateTimeProvider dateTimeProvider)
        : ITokenGenerator
    {
        public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Role, user.Role.ToString()),
                new(ClaimTypes.Email, user.Email)
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
}
