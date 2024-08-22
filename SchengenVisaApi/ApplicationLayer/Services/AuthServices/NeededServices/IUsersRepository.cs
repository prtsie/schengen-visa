using ApplicationLayer.InfrastructureServicesInterfaces;
using Domains.Users;

namespace ApplicationLayer.Services.AuthServices.NeededServices
{
    /// Repository pattern for <see cref="User"/>
    public interface IUsersRepository : IGenericRepository<User>
    {
        /// Find <see cref="User"/> by email
        /// <param name="email"><see cref="User"/>'s email</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>User or null if not found</returns>
        Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken);

        /// Returns all accounts with specific role
        /// <param name="role">role</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>list of accounts</returns>
        Task<List<User>> GetAllOfRoleAsync(Role role, CancellationToken cancellationToken);
    }
}
