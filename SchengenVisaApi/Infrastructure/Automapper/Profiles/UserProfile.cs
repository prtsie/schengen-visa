using ApplicationLayer.Services.AuthServices.Requests;
using AutoMapper;
using Domains.Users;

namespace Infrastructure.Automapper.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterApplicantRequest, User>(MemberList.Destination)
                .ForMember(u => u.Role,
                    opts => opts.Ignore());

            CreateMap<RegisterRequest, User>()
                .ForMember(u => u.Role,
                    opts => opts.Ignore());
        }
    }
}
