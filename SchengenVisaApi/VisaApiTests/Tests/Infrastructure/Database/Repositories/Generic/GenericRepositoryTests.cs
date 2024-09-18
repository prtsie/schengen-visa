using ApplicationLayer.GeneralExceptions;
using Domains.Users;
using FluentAssertions;
using Infrastructure.Database;
using Infrastructure.Database.Generic;
using Xunit;

namespace VisaApi.Tests.Infrastructure.Database.Repositories.Generic;

[Collection(Collections.ContextUsingTestCollection)]
public class GenericRepositoryTests
{
    /// <summary> Returns <see cref="GenericRepository{T}"/> </summary>
    /// <param name="context"> Database context </param>
    /// <returns>Repository</returns>
    private static GenericRepository<User> GetRepository(DbContext context) => new TestGenericRepository(context, context);

    /// <summary> Test for <see cref="GenericRepository{T}.GetAllAsync"/> method that should return empty collection if nothing added </summary>
    [Fact]
    public void GetAllForEmptyShouldReturnEmpty()
    {
        using var context = InMemoryContextProvider.GetDbContext();
        var repository = GetRepository(context);

        var result = repository.GetAllAsync(CancellationToken.None).Result;

        result.Should().BeEmpty();
    }

    /// <summary> Test for <see cref="GenericRepository{T}.GetAllAsync"/> method that should return collection with added entities </summary>
    [Fact]
    public void GetAllForNotEmptyShouldReturnEntities()
    {
        using var context = InMemoryContextProvider.GetDbContext();
        var repository = GetRepository(context);
        User[] users =
        [
            new() { Email = "nasrudin@mail.ru", Password = "12345", Role = Role.Admin },
            new() { Email = "bruh@mail.ru", Password = "123", Role = Role.Applicant }
        ];
        foreach (var user in users)
        {
            repository.AddAsync(user, CancellationToken.None).Wait();
        }

        context.SaveChanges();

        var result = repository.GetAllAsync(CancellationToken.None).Result;

        result.Should().Contain(users).And.HaveSameCount(users);
    }

    /// <summary> Test for <see cref="GenericRepository{T}.GetByIdAsync"/> method that should return existing entity </summary>
    [Fact]
    public void GetByIdForExistingShouldReturnEntity()
    {
        using var context = InMemoryContextProvider.GetDbContext();
        var repository = GetRepository(context);
        var user = new User { Email = "nasrudin@mail.ru", Password = "12345", Role = Role.Admin };
        repository.AddAsync(user, CancellationToken.None).Wait();

        context.SaveChanges();

        var result = repository.GetByIdAsync(user.Id, CancellationToken.None).Result;

        result.Should().Be(user);
    }

    /// <summary> Test for <see cref="GenericRepository{T}.GetByIdAsync"/> method that should throw exception for not found entity </summary>
    [Fact]
    public void GetByIdForNotExistingShouldThrow()
    {
        using var context = InMemoryContextProvider.GetDbContext();
        var repository = GetRepository(context);

        context.SaveChanges();

        EntityNotFoundByIdException<User>? result = null;
        try
        {
            repository.GetByIdAsync(Guid.NewGuid(), CancellationToken.None).Wait();
        }
        catch (AggregateException e)
        {
            result = e.InnerException as EntityNotFoundByIdException<User>;
        }

        result.Should().NotBeNull();
    }
}
