using ApplicationLayer.Services.AuthServices.Requests;

namespace ApplicationLayer.Services.AuthServices.LoginService
{
    /// Handles <see cref="UserLoginRequest"/>
    public interface ILoginService
    {
        /// Handle <see cref="UserLoginRequest"/>
        /// <returns>JWT-token</returns>
        Task<string> LoginAsync(UserLoginRequest request, CancellationToken cancellationToken);
    }
}
