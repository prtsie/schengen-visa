using ApplicationLayer.InfrastructureServicesInterfaces;

namespace VisaApi.Services;

public class TestDateTimeProvider : IDateTimeProvider
{
    public DateTime Now() => DateTime.Now;
}