using ApplicationLayer.Services.Applicants.Models;
using AutoMapper;
using Domains.ApplicantDomain;

namespace Infrastructure.Automapper.Profiles
{
    public class PlaceOfWorkProfile : Profile
    {
        public PlaceOfWorkProfile()
        {
            CreateMap<PlaceOfWorkModel, PlaceOfWork>(MemberList.Destination)
                .ForMember(p => p.Id,
                    opts => opts.UseDestinationValue());
        }
    }
}
