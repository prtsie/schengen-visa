using ApplicationLayer.Services.AuthServices.Common;

namespace ApplicationLayer.Services.Users.Requests
{
    public record ChangeUserAuthDataRequest(Guid UserId, AuthData NewAuthData);
}
