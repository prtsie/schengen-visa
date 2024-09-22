using AutoMapper;
using BlazorWebAssemblyVisaApiClient.Validation.Applicants.Models;
using VisaApiClient;
using PlaceOfWorkModel = BlazorWebAssemblyVisaApiClient.Validation.Applicants.Models.PlaceOfWorkModel;

namespace BlazorWebAssemblyVisaApiClient.Infrastructure.AutoMapper.Profiles;

public class RegisterApplicantRequestProfile : Profile
{
    public RegisterApplicantRequestProfile()
    {
            CreateMap<RegisterApplicantRequestModel, RegisterApplicantRequest>(MemberList.Destination);

            CreateMap<RegisterRequestModel, RegisterRequest>(MemberList.Destination);

            CreateMap<PlaceOfWorkModel, VisaApiClient.PlaceOfWorkModel>(MemberList.Destination);
        }
}