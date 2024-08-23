using ApplicationLayer.Services.Applicants.Models;
using AutoMapper;
using Domains.ApplicantDomain;

namespace Infrastructure.Automapper.Profiles
{
    public class ApplicantProfile : Profile
    {
        public ApplicantProfile()
        {
            CreateMap<Applicant, ApplicantModel>(MemberList.Destination);
        }
    }
}
