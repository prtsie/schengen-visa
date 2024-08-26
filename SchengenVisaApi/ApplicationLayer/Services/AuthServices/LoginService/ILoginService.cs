namespace ApplicationLayer.Services.AuthServices.LoginService;

/// Handles login requests
public interface ILoginService
{
    /// Handle login request
    /// <returns>JWT-token</returns>
    Task<string> LoginAsync(string email, string password, CancellationToken cancellationToken);
}