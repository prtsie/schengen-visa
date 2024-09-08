using BlazorWebAssemblyVisaApiClient.Common;

namespace BlazorWebAssemblyVisaApiClient.Infrastructure.Services.UserDataProvider.Exceptions
{
    public class UnknownRoleException() : BlazorClientException("Unknown user role");
}
