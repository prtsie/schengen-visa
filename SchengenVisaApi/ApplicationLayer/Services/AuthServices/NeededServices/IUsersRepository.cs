using ApplicationLayer.GeneralNeededServices;
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
    }
}
