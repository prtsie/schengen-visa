using VisaApiClient;

namespace BlazorWebAssemblyVisaApiClient.Infrastructure.Services.UserDataProvider
{
    public interface IUserDataProvider
    {

        public ApplicantModel? GetApplicant();

        public string? GetCurrentRole();
    }
}
