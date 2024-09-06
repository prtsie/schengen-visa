namespace BlazorWebAssemblyVisaApiClient.Infrastructure.Services.DateTimeProvider
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now() => DateTime.Now;

        public string FormattedNow() => Now().ToString("yyyy-MM-dd");
    }
}
