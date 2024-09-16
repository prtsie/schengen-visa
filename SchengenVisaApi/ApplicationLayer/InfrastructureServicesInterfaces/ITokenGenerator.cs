using ApplicationLayer.Services.AuthServices.Common;
using Domains.Users;

namespace ApplicationLayer.InfrastructureServicesInterfaces;

/// Generates jwt-tokens
public interface ITokenGenerator
{
    /// returns jwt-token for specific user
    AuthToken CreateToken(User user);
}
