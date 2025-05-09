using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace SchengenVisaApi;

#pragma warning disable CS1591
public class Program
{
    private const string MigrationKey = "--migrate";

    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.RegisterServices();

        var app = builder.Build();

        await HandleMigrationKey(args, app);

        app.ConfigurePipelineRequest();

        await app.RunAsync();
    }

    private static async Task HandleMigrationKey(string[] args, WebApplication app)
    {
        if (args.Contains(MigrationKey))
        {
            using var scope = app.Services.CreateScope();
            await using var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            await context.Database.MigrateAsync();
            Environment.Exit(0);
        }
    }
}
#pragma warning restore CS1591
