using Domains.Users;

namespace ApplicationLayer.AuthServices.NeededServices
{
    public interface ITokenGenerator
    {
        string CreateToken(User user);
    }
}
