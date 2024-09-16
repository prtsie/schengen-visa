using ApplicationLayer.Services.AuthServices.Common;
using ApplicationLayer.Services.Users.Models;
using AutoMapper;
using Domains.Users;

namespace Infrastructure.Automapper.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<AuthData, User>(MemberList.Destination)
            .ForMember(u => u.Role,
                opts => opts.Ignore());

        CreateMap<User, UserModel>(MemberList.Destination);
    }
}
