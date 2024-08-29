using System.ComponentModel.DataAnnotations;
using ApplicationLayer.Services.AuthServices.Common;

namespace ApplicationLayer.Services.AuthServices.Requests;

public class LoginRequest
{
    [Required] public AuthData AuthData { get; set; } = null!;
}
