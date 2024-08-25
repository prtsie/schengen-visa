﻿using ApplicationLayer.Services.AuthServices.LoginService.Exceptions;
using ApplicationLayer.Services.AuthServices.NeededServices;
using Domains.Users;

namespace ApplicationLayer.Services.AuthServices.LoginService
{
    public class DevelopmentLoginService(IUsersRepository users, ITokenGenerator tokenGenerator) : ILoginService
    {
        async Task<string> ILoginService.LoginAsync(string email, string password, CancellationToken cancellationToken)
        {
            if (email == "admin@mail.ru" && password == "admin")
            {
                var admin = new User { Role = Role.Admin };

                return tokenGenerator.CreateToken(admin);
            }

            var user = await users.FindByEmailAsync(email, cancellationToken);
            if (user is null || user.Password != password)
            {
                throw new IncorrectLoginDataException();
            }

            return tokenGenerator.CreateToken(user);
        }
    }
}
