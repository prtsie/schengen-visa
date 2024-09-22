using System.ComponentModel.DataAnnotations;
using Domains;

namespace ApplicationLayer.Services.Users.Models;

/// Auth data with nullable password for making change auth data requests
public class ChangeAuthData
{
    [Required]
    [MaxLength(ConfigurationConstraints.EmailLength)]
    public string Email { get; set; } = null!;

    [MaxLength(ConfigurationConstraints.PasswordLength)]
    public string? Password { get; set; }
}