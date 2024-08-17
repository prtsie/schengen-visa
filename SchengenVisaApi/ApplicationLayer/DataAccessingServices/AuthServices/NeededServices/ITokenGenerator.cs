using Domains.Users;

namespace ApplicationLayer.DataAccessingServices.AuthServices.NeededServices
{
    public interface ITokenGenerator
    {
        string CreateToken(User user);
    }
}
