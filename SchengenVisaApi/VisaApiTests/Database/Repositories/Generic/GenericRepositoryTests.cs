using ApplicationLayer.GeneralExceptions;
using Domains.Users;
using FluentAssertions;
using Infrastructure.Database;
using Infrastructure.Database.Generic;

namespace VisaApi.Database.Repositories.Generic;

public class GenericRepositoryTests
{
    /// <summary> Returns <see cref="GenericRepository{T}"/> </summary>
    /// <param name="context"> Database context </param>
    /// <returns>Repository</returns>
    private static GenericRepository<User> GetRepository(DbContext context) => new TestGenericRepository(context, context);

    /// <summary> Test for <see cref="GenericRepository{T}.GetAllAsync"/> method that should return empty collection if nothing added </summary>
    [Fact]
    public async Task GetAllForEmptyShouldReturnEmpty()
    {
        await using var context = InMemoryContextProvider.GetDbContext();
        var repository = GetRepository(context);

        var result = await repository.GetAllAsync(CancellationToken.None);

        result.Should().BeEmpty();
    }

    /// <summary> Test for <see cref="GenericRepository{T}.GetAllAsync"/> method that should return collection with added entities </summary>
    [Fact]
    public async Task GetAllForNotEmptyShouldReturnEntities()
    {
        await using var context = InMemoryContextProvider.GetDbContext();
        var repository = GetRepository(context);
        User[] users =
        [
            new() { Email = "nasrudin@mail.ru", Password = "12345", Role = Role.Admin },
            new() { Email = "bruh@mail.ru", Password = "123", Role = Role.Applicant }
        ];
        foreach (var user in users)
        {
            await repository.AddAsync(user, CancellationToken.None);
        }

        await context.SaveChangesAsync();

        var result = await repository.GetAllAsync(CancellationToken.None);

        result.Should().OnlyContain(user => users.Contains(user));
    }

    /// <summary> Test for <see cref="GenericRepository{T}.GetByIdAsync"/> method that should return existing entity </summary>
    [Fact]
    public async Task GetByIdForExistingShouldReturnEntity()
    {
        await using var context = InMemoryContextProvider.GetDbContext();
        var repository = GetRepository(context);
        var user = new User { Email = "nasrudin@mail.ru", Password = "12345", Role = Role.Admin };
        await repository.AddAsync(user, CancellationToken.None);

        await context.SaveChangesAsync();

        var result = await repository.GetByIdAsync(user.Id, CancellationToken.None);

        result.Should().Be(user);
    }

    /// <summary> Test for <see cref="GenericRepository{T}.GetByIdAsync"/> method that should throw exception for not found entity </summary>
    [Fact]
    public async Task GetByIdForNotExistingShouldThrow()
    {
        await using var context = InMemoryContextProvider.GetDbContext();
        var repository = GetRepository(context);

        await context.SaveChangesAsync();

        EntityNotFoundByIdException<User>? result = null;
        try
        {
            await repository.GetByIdAsync(Guid.NewGuid(), CancellationToken.None);
        }
        catch (Exception e)
        {
            result = e as EntityNotFoundByIdException<User>;
        }

        result.Should().NotBeNull();
    }
}
