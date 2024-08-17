using ApplicationLayer.DataAccessingServices.AuthServices.LoginService.Exceptions;
using ApplicationLayer.DataAccessingServices.AuthServices.NeededServices;
using ApplicationLayer.DataAccessingServices.AuthServices.Requests;
using Domains.Users;

namespace ApplicationLayer.DataAccessingServices.AuthServices.LoginService
{
    public class DevelopmentLoginService(IUsersRepository users, ITokenGenerator tokenGenerator) : ILoginService
    {
        async Task<string> ILoginService.LoginAsync(UserLoginRequest request, CancellationToken cancellationToken)
        {
            if (request is { Email: "admin@mail.ru", Password: "admin" })
            {
                var admin = new User { Role = Role.Admin };

                return tokenGenerator.CreateToken(admin);
            }

            var user = await users.FindByEmailAsync(request.Email, cancellationToken);
            if (user is null || user.Password != request.Password)
            {
                throw new IncorrectLoginDataException();
            }

            return tokenGenerator.CreateToken(user);
        }
    }
}
