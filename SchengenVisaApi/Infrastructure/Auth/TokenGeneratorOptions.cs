using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Auth
{
    public record TokenGeneratorOptions(string Issuer, string Audience, TimeSpan ValidTime, SigningCredentials Credentials);
}
