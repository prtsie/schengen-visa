using ApplicationLayer.AuthServices.NeededServices;
using Domains.Users;
using Infrastructure.Database.Generic;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Users.Repositories
{
    /// <inheritdoc cref="IUsersRepository"/>
    public class UsersRepository(IGenericReader reader, IGenericWriter writer, IUnitOfWork unitOfWork)
        : GenericRepository<User>(reader, writer, unitOfWork), IUsersRepository
    {
        async Task<User?> IUsersRepository.FindByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await LoadDomain().SingleOrDefaultAsync(u => u.Email == email, cancellationToken);
        }
    }
}
