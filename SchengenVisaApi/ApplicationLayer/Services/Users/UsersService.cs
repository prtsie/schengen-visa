﻿using ApplicationLayer.InfrastructureServicesInterfaces;
using ApplicationLayer.Services.AuthServices.Common;
using ApplicationLayer.Services.AuthServices.NeededServices;
using ApplicationLayer.Services.Users.Exceptions;
using ApplicationLayer.Services.Users.Requests;
using Domains.Users;

namespace ApplicationLayer.Services.Users;

public class UsersService(IUsersRepository users, IUnitOfWork unitOfWork) : IUsersService
{
    async Task<List<User>> IUsersService.GetAuthoritiesAccountsAsync(CancellationToken cancellationToken) =>
        await users.GetAllOfRoleAsync(Role.ApprovingAuthority, cancellationToken);

    async Task IUsersService.ChangeAuthorityAuthDataAsync(ChangeUserAuthDataRequest request, CancellationToken cancellationToken)
    {
        var user = await users.GetByIdAsync(request.UserId, cancellationToken);

        ValidateRole(user, Role.ApprovingAuthority);

        await ChangeAccountAuthDataAsync(user, request.NewAuthData, cancellationToken);
    }

    async Task IUsersService.RemoveAuthorityAccount(Guid userId, CancellationToken cancellationToken)
    {
        var user = await users.GetByIdAsync(userId, cancellationToken);

        ValidateRole(user, Role.ApprovingAuthority);

        await RemoveUserAccount(user, cancellationToken);
    }

    /// Updates user account auth data
    /// <param name="user">User to remove</param>
    /// <param name="authData">New auth data</param>
    /// <param name="cancellationToken">Cancellation token</param>
    private async Task ChangeAccountAuthDataAsync(User user, AuthData authData, CancellationToken cancellationToken)
    {
        user.Email = authData.Email;
        user.Password = authData.Password;
        await users.UpdateAsync(user, cancellationToken);

        await unitOfWork.SaveAsync(cancellationToken);
    }

    /// Removes user account from DB
    /// <param name="user">User to remove</param>
    /// <param name="cancellationToken">Cancellation token</param>
    private async Task RemoveUserAccount(User user, CancellationToken cancellationToken)
    {
        users.Remove(user);

        await unitOfWork.SaveAsync(cancellationToken);
    }

    /// Checks if role of user equals expected
    /// <param name="user">User to check</param>
    /// <param name="expectedRole">Expected role</param>
    /// <exception cref="WrongRoleException">Role is not expected</exception>
    private static void ValidateRole(User user, Role expectedRole)
    {
        if (user.Role != expectedRole)
        {
            throw new WrongRoleException(user.Id);
        }
    }
}
