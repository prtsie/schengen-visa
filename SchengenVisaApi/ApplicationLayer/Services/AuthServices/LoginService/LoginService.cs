using ApplicationLayer.Services.AuthServices.LoginService.Exceptions;
using ApplicationLayer.Services.AuthServices.NeededServices;
using ApplicationLayer.Services.AuthServices.Requests;

namespace ApplicationLayer.Services.AuthServices.LoginService;

/// <inheritdoc cref="ILoginService" />
public class LoginService(IUsersRepository users, ITokenGenerator tokenGenerator) : ILoginService
{
    async Task<string> ILoginService.LoginAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await users.FindByEmailAsync(request.AuthData.Email, cancellationToken);
        if (user is null || user.Password != request.AuthData.Password)
        {
            throw new IncorrectLoginDataException();
        }

        return tokenGenerator.CreateToken(user);
    }
}
