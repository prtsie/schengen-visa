using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using VisaApiClient;

namespace BlazorWebAssemblyVisaApiClient.Infrastructure.Services.UserDataProvider
{
    public class UserDataProvider(Client client) : IUserDataProvider
    {
        private static readonly JwtSecurityTokenHandler tokenHandler = new ();

        public ApplicantModel? GetApplicant()
        {
            //todo api action
            return null;
        }

        public string? GetCurrentRole()
        {
            if (client.AuthToken is null)
            {
                return null;
            }

            var token = tokenHandler.ReadJwtToken(client.AuthToken.Token);
            var role = token.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role)?.Value;

            return role;
        }
    }
}
