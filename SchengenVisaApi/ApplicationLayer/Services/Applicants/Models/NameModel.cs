using System.ComponentModel.DataAnnotations;
using Domains;

namespace ApplicationLayer.Services.Applicants.Models;

/// Model of name for presentation layer
public class NameModel
{
    [Required]
    [MaxLength(ConfigurationConstraints.NameLength)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(ConfigurationConstraints.NameLength)]
    public string Surname { get; set; } = null!;

    [MaxLength(ConfigurationConstraints.NameLength)]
    public string? Patronymic { get; set; }

    public override string ToString() => $"{FirstName} {Surname} {Patronymic}".TrimEnd();
}
