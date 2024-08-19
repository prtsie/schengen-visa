using ApplicationLayer.Services.AuthServices.LoginService.Exceptions;
using ApplicationLayer.Services.AuthServices.NeededServices;
using ApplicationLayer.Services.AuthServices.Requests;

namespace ApplicationLayer.Services.AuthServices.LoginService
{
    /// <inheritdoc cref="ILoginService"/>
    public class LoginService(IUsersRepository users, ITokenGenerator tokenGenerator) : ILoginService
    {
        async Task<string> ILoginService.LoginAsync(UserLoginRequest request, CancellationToken cancellationToken)
        {
            var user = await users.FindByEmailAsync(request.Email, cancellationToken);
            if (user is null || user.Password != request.Password)
            {
                throw new IncorrectLoginDataException();
            }

            return tokenGenerator.CreateToken(user);
        }
    }
}
