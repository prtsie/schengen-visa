using ApplicationLayer.Services.Applicants.Models;
using ApplicationLayer.Services.AuthServices.Requests;
using AutoMapper;
using Domains.ApplicantDomain;

namespace Infrastructure.Automapper.Profiles;

public class ApplicantProfile : Profile
{
    public ApplicantProfile()
    {
            CreateMap<Applicant, ApplicantModel>(MemberList.Destination);

            CreateMap<RegisterApplicantRequest, Applicant>(MemberList.Destination)
                .ForMember(a => a.UserId, opts => opts.Ignore())
                .ForMember(a => a.Name,
                    opts => opts.MapFrom(r => r.ApplicantName));
        }
}