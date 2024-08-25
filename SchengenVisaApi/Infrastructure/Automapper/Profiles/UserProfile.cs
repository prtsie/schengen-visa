using ApplicationLayer.Services.AuthServices.Common;
using AutoMapper;
using Domains.Users;

namespace Infrastructure.Automapper.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AuthData, User>(MemberList.Destination)
                .ForMember(u => u.Role,
                    opts => opts.Ignore());
        }
    }
}
