using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace VisaApi.Tests.Infrastructure.Database;

public static class InMemoryContextProvider
{
    private static DbContextOptions<DatabaseContext> opts = new DbContextOptionsBuilder<DatabaseContext>()
        .UseInMemoryDatabase("VisaApiDB")
        .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
        .Options;

    public static DatabaseContext GetDbContext()
    {
            var result = new DatabaseContext(opts);

            result.Database.EnsureDeleted();
            result.Database.EnsureCreated();

            return result;
        }
}