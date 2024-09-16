using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using DbContext = Infrastructure.Database.DbContext;

namespace VisaApi.Database
{
    public static class InMemoryContextProvider
    {
        private static DbContextOptions<DbContext> opts = new DbContextOptionsBuilder<DbContext>()
        .UseInMemoryDatabase("VisaApiDB")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;

        public static DbContext GetDbContext() => new(opts);
    }
}
