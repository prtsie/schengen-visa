using System.ComponentModel.DataAnnotations;

namespace ApplicationLayer.Services.AuthServices.Common
{
    public class AuthToken
    {
        [Required] public string Token { get; set; } = null!;
    }
}
