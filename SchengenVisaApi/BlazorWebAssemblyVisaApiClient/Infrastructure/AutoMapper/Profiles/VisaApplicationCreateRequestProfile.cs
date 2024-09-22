using AutoMapper;
using BlazorWebAssemblyVisaApiClient.Validation.VisaApplications.Models;
using VisaApiClient;

namespace BlazorWebAssemblyVisaApiClient.Infrastructure.AutoMapper.Profiles;

public class VisaApplicationCreateRequestProfile : Profile
{
    public VisaApplicationCreateRequestProfile()
    {
            CreateMap<VisaApplicationCreateRequestModel, VisaApplicationCreateRequest>(MemberList.Destination);
        }
}