using AutoMapper;
using BlazorWebAssemblyVisaApiClient.FluentValidation.Applicants.Models;
using VisaApiClient;

namespace BlazorWebAssemblyVisaApiClient.Infrastructure.AutoMapper.Profiles
{
    public class RegisterApplicantRequestProfile : Profile
    {
        public RegisterApplicantRequestProfile()
        {
            CreateMap<RegisterApplicantRequestModel, RegisterApplicantRequest>(MemberList.Destination);

            CreateMap<RegisterRequestModel, RegisterRequest>(MemberList.Destination);

            CreateMap<FluentValidation.Applicants.Models.PlaceOfWorkModel, VisaApiClient.PlaceOfWorkModel>(MemberList.Destination);
        }
    }
}
