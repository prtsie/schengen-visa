using ApplicationLayer.InfrastructureServicesInterfaces;
using ApplicationLayer.Services.AuthServices.NeededServices;
using ApplicationLayer.Services.AuthServices.Requests;
using Domains.Users;

namespace ApplicationLayer.Services.ApprovingAuthorities
{
    public class UsersService(IUsersRepository users, IUnitOfWork unitOfWork) : IUsersService
    {
        async Task<List<User>> IUsersService.GetAuthoritiesAccountsAsync(CancellationToken cancellationToken)
        {
            return await users.GetAllOfRoleAsync(Role.ApprovingAuthority, cancellationToken);
        }

        async Task IUsersService.ChangeAccountAuthDataAsync(Guid userId, RegisterRequest data, CancellationToken cancellationToken)
        {
            var user = await users.GetByIdAsync(userId, cancellationToken);

            user.Email = data.Email;
            user.Password = data.Password;
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
}
