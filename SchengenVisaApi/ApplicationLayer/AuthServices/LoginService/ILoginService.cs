using ApplicationLayer.AuthServices.Requests;

namespace ApplicationLayer.AuthServices.LoginService
{
    /// Handles <see cref="UserLoginRequest"/>
    public interface ILoginService
    {
        /// Handle <see cref="UserLoginRequest"/>
        /// <returns>JWT-token</returns>
        Task<string> LoginAsync(UserLoginRequest request, CancellationToken cancellationToken);
    }
}
