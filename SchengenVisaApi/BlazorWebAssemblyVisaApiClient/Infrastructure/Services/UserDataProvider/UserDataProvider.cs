using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BlazorWebAssemblyVisaApiClient.Infrastructure.Services.UserDataProvider.Exceptions;
using VisaApiClient;

namespace BlazorWebAssemblyVisaApiClient.Infrastructure.Services.UserDataProvider
{
    public class UserDataProvider(Client client) : IUserDataProvider
    {
        private readonly static JwtSecurityTokenHandler tokenHandler = new();

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

            switch (role)
            {
                case Constants.ApplicantRole: break;
                case Constants.ApprovingAuthorityRole: break;
                case Constants.AdminRole: break;
                default: throw new UnknownRoleException();
            }

            return role;
        }
    }
}
