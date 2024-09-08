using BlazorWebAssemblyVisaApiClient.Common;

namespace BlazorWebAssemblyVisaApiClient.PagesExceptions.Applications
{
    public class UnknownApplicationTypeException() : BlazorClientException("Application type is unknown");
}
