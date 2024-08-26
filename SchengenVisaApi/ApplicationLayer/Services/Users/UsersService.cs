using ApplicationLayer.InfrastructureServicesInterfaces;
using ApplicationLayer.Services.AuthServices.NeededServices;
using ApplicationLayer.Services.Users.Requests;
using Domains.Users;

namespace ApplicationLayer.Services.Users;

public class UsersService(IUsersRepository users, IUnitOfWork unitOfWork) : IUsersService
{
    async Task<List<User>> IUsersService.GetAuthoritiesAccountsAsync(CancellationToken cancellationToken)
    {
            return await users.GetAllOfRoleAsync(Role.ApprovingAuthority, cancellationToken);
        }

    async Task IUsersService.ChangeAccountAuthDataAsync(ChangeUserAuthDataRequest request, CancellationToken cancellationToken)
    {
            var user = await users.GetByIdAsync(request.UserId, cancellationToken);

            user.Email = request.NewAuthData.Email;
            user.Password = request.NewAuthData.Password;
            await users.UpdateAsync(user, cancellationToken);

            await unitOfWork.SaveAsync(cancellationToken);
        }

    async Task IUsersService.RemoveUserAccount(Guid userId, CancellationToken cancellationToken)
    {
            var user = await users.GetByIdAsync(userId, cancellationToken);
            users.Remove(user);

            await unitOfWork.SaveAsync(cancellationToken);
        }
}