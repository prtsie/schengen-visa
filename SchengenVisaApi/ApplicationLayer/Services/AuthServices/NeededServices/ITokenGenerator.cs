using Domains.Users;

namespace ApplicationLayer.Services.AuthServices.NeededServices
{
    public interface ITokenGenerator
    {
        string CreateToken(User user);
    }
}
