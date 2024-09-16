using VisaApiClient;

namespace BlazorWebAssemblyVisaApiClient.Infrastructure.Services.UserDataProvider
{
    public interface IUserDataProvider
    {
        public string? CurrentRole { get; }

        public Action? OnRoleChanged { get; set; }

        public Task<ApplicantModel> GetApplicant();

        public void UpdateCurrentRole();
    }
}
