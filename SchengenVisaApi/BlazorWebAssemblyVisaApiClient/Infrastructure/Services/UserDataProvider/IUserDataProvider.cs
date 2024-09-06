using VisaApiClient;

namespace BlazorWebAssemblyVisaApiClient.Infrastructure.Services.UserDataProvider
{
    public interface IUserDataProvider
    {

        public Task<ApplicantModel> GetApplicant();

        public string? GetCurrentRole();
    }
}
