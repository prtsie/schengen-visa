namespace BlazorWebAssemblyVisaApiClient.Infrastructure.Services.DateTimeProvider;

public interface IDateTimeProvider
{
    DateTime Now();

    string FormattedNow();
}