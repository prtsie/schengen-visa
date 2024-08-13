namespace SchengenVisaApi;

#pragma warning disable CS1591
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.RegisterServices();

        var app = builder.Build();
        app.ConfigurePipelineRequest();

        app.Run();
    }
}
#pragma warning restore CS1591
