using System.Reflection;
using BlazorWebAssemblyVisaApiClient.Infrastructure.Services.DateTimeProvider;
using BlazorWebAssemblyVisaApiClient.Infrastructure.Services.UserDataProvider;
using FluentValidation;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using VisaApiClient;

namespace BlazorWebAssemblyVisaApiClient;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.AddInfrastructure();

        builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        await builder.Build().RunAsync();
    }

    private static void AddInfrastructure(this WebAssemblyHostBuilder builder)
    {
        const string baseAddress = "https://localhost:44370";
        builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new(baseAddress) });
        builder.Services.AddBlazorBootstrap();
        builder.Services.AddScoped<IClient, Client>(sp => new(baseAddress, sp.GetRequiredService<HttpClient>()));

        builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        builder.Services.AddScoped<IUserDataProvider, UserDataProvider>();
        builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
    }
}
