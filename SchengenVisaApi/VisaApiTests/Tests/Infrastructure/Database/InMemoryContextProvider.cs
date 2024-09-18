using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using DbContext = Infrastructure.Database.DbContext;

namespace VisaApi.Tests.Infrastructure.Database
{
    public static class InMemoryContextProvider
    {
        private static DbContextOptions<DbContext> opts = new DbContextOptionsBuilder<DbContext>()
        .UseInMemoryDatabase("VisaApiDB")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        public static DbContext GetDbContext()
        {
            var result = new DbContext(opts);

            result.Database.EnsureDeleted();
            result.Database.EnsureCreated();

            return result;
        }
    }
}
