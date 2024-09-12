using System.ComponentModel.DataAnnotations;
using ApplicationLayer.Services.Users.Models;

namespace ApplicationLayer.Services.Users.Requests;

public class ChangeUserAuthDataRequest(Guid userId, ChangeAuthData newAuthData)
{
    [Required] public Guid UserId { get; set; } = userId;

    [Required] public ChangeAuthData NewAuthData { get; set; } = newAuthData;
}
