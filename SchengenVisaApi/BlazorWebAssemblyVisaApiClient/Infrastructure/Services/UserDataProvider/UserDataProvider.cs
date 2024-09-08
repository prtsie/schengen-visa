using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BlazorWebAssemblyVisaApiClient.Infrastructure.Services.UserDataProvider.Exceptions;
using VisaApiClient;

namespace BlazorWebAssemblyVisaApiClient.Infrastructure.Services.UserDataProvider
{
    public class UserDataProvider(Client client) : IUserDataProvider
    {
        private readonly static JwtSecurityTokenHandler tokenHandler = new ();
        private readonly static string[] knownRoles = ["Applicant", "ApprovingAuthority", "Admin"];

        public async Task<ApplicantModel> GetApplicant()
        {
            return await client.GetApplicantAsync();
        }

        public string? GetCurrentRole()
        {
            if (client.AuthToken is null)
            {
                return null;
            }

            var token = tokenHandler.ReadJwtToken(client.AuthToken.Token);
            var role = token.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role)?.Value;

            if (!knownRoles.Contains(role))
            {
                throw new UnknownRoleException();
            }

            return role;
        }
    }
}
