using BlazorWebAssemblyVisaApiClient.Common;

namespace BlazorWebAssemblyVisaApiClient.Components.Auth.Exceptions
{
    public class NotLoggedInException() : BlazorClientException("User is not logged in");
}
