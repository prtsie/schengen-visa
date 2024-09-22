using System.ComponentModel.DataAnnotations;
using Domains;

namespace ApplicationLayer.Services.Users.Models;

public class UserModel
{
    /// Unique Identifier of user
    [Required]
    public Guid Id { get; private set; } = Guid.NewGuid();

    [Required]
    [MaxLength(ConfigurationConstraints.EmailLength)]
    public string Email { get; set; } = null!;
}