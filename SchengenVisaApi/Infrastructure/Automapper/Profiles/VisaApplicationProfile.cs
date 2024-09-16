using ApplicationLayer.Services.VisaApplications.Models;
using ApplicationLayer.Services.VisaApplications.Requests;
using AutoMapper;
using Domains.VisaApplicationDomain;

namespace Infrastructure.Automapper.Profiles;

public class VisaApplicationProfile : Profile
{
    public VisaApplicationProfile()
    {
        CreateMap<VisaApplication, VisaApplicationPreview>(MemberList.Destination);

        CreateMap<VisaApplication, VisaApplicationModel>(MemberList.Destination)
            .ForMember(model => model.Applicant,
                opts => opts.Ignore());

        CreateMap<VisaApplicationCreateRequest, VisaApplication>(MemberList.Destination)
            .ForMember(va => va.RequestDate,
                opts => opts.Ignore())
            .ForMember(va => va.ApplicantId,
                opts => opts.Ignore());

        CreateMap<PastVisaModel, PastVisa>().ReverseMap();
        CreateMap<PastVisitModel, PastVisit>().ReverseMap();
        CreateMap<ReentryPermitModel, ReentryPermit>().ReverseMap();
        CreateMap<PermissionToDestCountryModel, PermissionToDestCountry>().ReverseMap();
    }
}
