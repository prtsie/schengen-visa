using ApplicationLayer.InfrastructureServicesInterfaces;
using ApplicationLayer.Services.Applicants.Models;
using ApplicationLayer.Services.Applicants.NeededServices;
using ApplicationLayer.Services.AuthServices.Common;
using ApplicationLayer.Services.AuthServices.NeededServices;
using ApplicationLayer.Services.Users.Exceptions;
using ApplicationLayer.Services.Users.Models;
using ApplicationLayer.Services.Users.Requests;
using AutoMapper;
using Domains.Users;

namespace ApplicationLayer.Services.Users;

public class UsersService(
    IMapper mapper,
    IUserIdProvider userIdProvider,
    IUsersRepository users,
    IApplicantsRepository applicants,
    IUnitOfWork unitOfWork) : IUsersService
{
    async Task<List<UserModel>> IUsersService.GetAuthoritiesAccountsAsync(CancellationToken cancellationToken)
    {
        var userList = await users.GetAllOfRoleAsync(Role.ApprovingAuthority, cancellationToken);
        return mapper.Map<List<UserModel>>(userList);
    }

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

    async Task<ApplicantModel> IUsersService.GetAuthenticatedApplicant(CancellationToken cancellationToken)
    {
        var applicant = await applicants.FindByUserIdAsync(userIdProvider.GetUserId(), cancellationToken);

        return mapper.Map<ApplicantModel>(applicant);
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
