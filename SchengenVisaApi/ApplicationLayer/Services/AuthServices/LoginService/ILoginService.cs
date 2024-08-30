using ApplicationLayer.Services.AuthServices.Common;
using ApplicationLayer.Services.AuthServices.Requests;

namespace ApplicationLayer.Services.AuthServices.LoginService;

/// Handles login requests
public interface ILoginService
{
    /// Handle login request
    /// <returns>JWT-token</returns>
    Task<AuthToken> LoginAsync(LoginRequest request, CancellationToken cancellationToken);
}
