using System.Reflection;
using BlazorWebAssemblyVisaApiClient.Infrastructure.Services;
using FluentValidation;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using VisaApiClient;

namespace BlazorWebAssemblyVisaApiClient;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        //todo move to launch settings
        const string baseAddress = "https://localhost:44370";

        //todo make pretty
        builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(baseAddress) });
        builder.Services.AddScoped<Client>(sp => new Client(baseAddress, sp.GetRequiredService<HttpClient>()));

        builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

        builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        await builder.Build().RunAsync();
    }
}
