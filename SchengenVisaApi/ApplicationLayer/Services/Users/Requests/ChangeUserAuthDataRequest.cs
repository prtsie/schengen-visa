using System.ComponentModel.DataAnnotations;
using ApplicationLayer.Services.AuthServices.Common;

namespace ApplicationLayer.Services.Users.Requests;

public class ChangeUserAuthDataRequest(Guid userId, AuthData newAuthData)
{
    [Required] public Guid UserId { get; set; } = userId;

    [Required] public AuthData NewAuthData { get; set; } = newAuthData;
}
