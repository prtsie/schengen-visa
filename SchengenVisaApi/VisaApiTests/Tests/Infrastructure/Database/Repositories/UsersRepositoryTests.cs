using ApplicationLayer.Services.AuthServices.NeededServices;
using ApplicationLayer.Services.VisaApplications.NeededServices;
using Domains.Users;
using FluentAssertions;
using Infrastructure.Database;
using Infrastructure.Database.Users.Repositories;
using VisaApi.Fakers.Users;
using Xunit;

namespace VisaApi.Tests.Infrastructure.Database.Repositories
{
    [Collection(Collections.ContextUsingTestCollection)]
    public class UsersRepositoryTests
    {
        private readonly static UserFaker userFaker = new();

        /// <summary> Returns <see cref="IVisaApplicationsRepository"/> </summary>
        /// <param name="context"> Database context </param>
        /// <returns>Repository</returns>
        private static IUsersRepository GetRepository(DbContext context)
            => new UsersRepository(context, context);

        /// <summary>
        /// Test for <see cref="IUsersRepository.FindByEmailAsync"/> method that should return null for not existing email
        /// </summary>
        [Fact]
        private async Task FindByEmailForNotExistingShouldReturnNull()
        {
            await using var context = InMemoryContextProvider.GetDbContext();
            var repository = GetRepository(context);

            var result = await repository.FindByEmailAsync("email@email.ru", CancellationToken.None);

            result.Should().BeNull();
        }

        /// <summary>
        /// Test for <see cref="IUsersRepository.FindByEmailAsync"/> method that should return entity for existing email
        /// </summary>
        [Fact]
        private async Task FindByEmailForExistingShouldReturnEntity()
        {
            await using var context = InMemoryContextProvider.GetDbContext();
            var repository = GetRepository(context);
            var user = userFaker.Generate();
            await repository.AddAsync(user, CancellationToken.None);
            await context.SaveChangesAsync();

            var result = await repository.FindByEmailAsync(user.Email, CancellationToken.None);

            result.Should().Be(user);
        }

        /// <summary>
        /// Test for <see cref="IUsersRepository.GetAllOfRoleAsync"/> method that should return empty from empty db
        /// </summary>
        [Fact]
        private async Task GetAllOfRoleForEmptyShouldReturnEmpty()
        {
            await using var context = InMemoryContextProvider.GetDbContext();
            var repository = GetRepository(context);

            var result = await repository.GetAllOfRoleAsync(Role.ApprovingAuthority, CancellationToken.None);

            result.Should().BeEmpty();
        }

        /// <summary>
        /// Test for <see cref="IUsersRepository.GetAllOfRoleAsync"/> method that should return entities from not empty db
        /// </summary>
        [Fact]
        private async Task GetAllOfRoleForNotEmptyShouldReturnEntities()
        {
            await using var context = InMemoryContextProvider.GetDbContext();
            var repository = GetRepository(context);
            var users = new List<User>();
            for (var i = 0; i < 3; i++)
            {
                var user = userFaker.Generate();
                user.Role = Role.ApprovingAuthority;
                users.Add(user);
                await repository.AddAsync(user, CancellationToken.None);
            }

            await context.SaveChangesAsync();

            var result = await repository.GetAllOfRoleAsync(Role.ApprovingAuthority, CancellationToken.None);

            result.Should().Contain(users).And.HaveSameCount(users);
        }
    }
}
