using ApplicationLayer.Services.AuthServices.LoginService.Exceptions;
using ApplicationLayer.Services.AuthServices.NeededServices;

namespace ApplicationLayer.Services.AuthServices.LoginService
{
    /// <inheritdoc cref="ILoginService"/>
    public class LoginService(IUsersRepository users, ITokenGenerator tokenGenerator) : ILoginService
    {
        async Task<string> ILoginService.LoginAsync(string email, string password, CancellationToken cancellationToken)
        {
            var user = await users.FindByEmailAsync(email, cancellationToken);
            if (user is null || user.Password != password)
            {
                throw new IncorrectLoginDataException();
            }

            return tokenGenerator.CreateToken(user);
        }
    }
}
