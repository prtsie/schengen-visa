using Domains.Users;

namespace ApplicationLayer.Services.AuthServices.NeededServices;

/// Generates jwt-tokens
public interface ITokenGenerator
{
    /// returns jwt-token for specific user
    string CreateToken(User user);
}
