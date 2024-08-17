using ApplicationLayer.AuthServices.LoginService.Exceptions;
using ApplicationLayer.AuthServices.NeededServices;
using ApplicationLayer.AuthServices.Requests;

namespace ApplicationLayer.AuthServices.LoginService
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
