using BlazorWebAssemblyVisaApiClient.Common.Exceptions;

namespace BlazorWebAssemblyVisaApiClient.Infrastructure.Services.UserDataProvider.Exceptions
{
    public class UnknownRoleException() : BlazorClientException("Unknown user role");
}
