using ApplicationLayer.Services.AuthServices.NeededServices;
using Domains.Users;
using Infrastructure.Database.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Users.Repositories
{
    /// <inheritdoc cref="IUsersRepository"/>
    public class UsersRepository(IGenericReader reader, IGenericWriter writer)
        : GenericRepository<User>(reader, writer), IUsersRepository
    {
        async Task<User?> IUsersRepository.FindByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await LoadDomain().SingleOrDefaultAsync(u => u.Email == email, cancellationToken);
        }

        async Task<List<User>> IUsersRepository.GetAllOfRoleAsync(Role role, CancellationToken cancellationToken)
        {
            return await LoadDomain().Where(u => u.Role == role).ToListAsync(cancellationToken);
        }
    }
}
