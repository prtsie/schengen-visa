using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BlazorWebAssemblyVisaApiClient.Infrastructure.Services.UserDataProvider.Exceptions;
using VisaApiClient;

namespace BlazorWebAssemblyVisaApiClient.Infrastructure.Services.UserDataProvider;

public class UserDataProvider(Client client) : IUserDataProvider
{
    private readonly static JwtSecurityTokenHandler tokenHandler = new();

    public string? CurrentRole { get; private set; }

    public Action? OnRoleChanged { get; set; }

    public async Task<ApplicantModel> GetApplicant()
    {
            return await client.GetApplicantAsync();
        }

    public void UpdateCurrentRole()
    {
            var role = CurrentRole;

            if (client.AuthToken is null)
            {
                if (CurrentRole is not null)
                {
                    role = null;
                }
            }
            else
            {
                var token = tokenHandler.ReadJwtToken(client.AuthToken.Token);
                role = token.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Role)?.Value;

                switch (role)
                {
                    case Constants.ApplicantRole: break;
                    case Constants.ApprovingAuthorityRole: break;
                    case Constants.AdminRole: break;
                    default: throw new UnknownRoleException();
                }
            }

            if (CurrentRole != role)
            {
                CurrentRole = role;
                OnRoleChanged?.Invoke();
            }
        }
}