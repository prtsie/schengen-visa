using System.ComponentModel.DataAnnotations;
using Domains;

namespace ApplicationLayer.Services.AuthServices.Common;

public class AuthData
{
    [Required]
    [MaxLength(ConfigurationConstraints.EmailLength)]
    public string Email { get; set; } = null!;

    [Required]
    [MaxLength(ConfigurationConstraints.PasswordLength)]
    public string Password { get; set; } = null!;
}
