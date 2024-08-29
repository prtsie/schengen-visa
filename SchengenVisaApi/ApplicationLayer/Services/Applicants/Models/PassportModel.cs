using System.ComponentModel.DataAnnotations;
using Domains;

namespace ApplicationLayer.Services.Applicants.Models;

/// Model of passport fpr presentation layer
public class PassportModel
{
    /// Number of passport
    [Required]
    [MaxLength(ConfigurationConstraints.PassportNumberLength)]
    public string Number { get; set; } = null!;

    /// Issuing authority of passport
    [Required]
    [MaxLength(ConfigurationConstraints.IssuerNameLength)]
    public string Issuer { get; set; } = null!;

    /// Date of issue
    [Required]
    public DateTime IssueDate { get; set; }

    /// Date when the passport expires
    [Required]
    public DateTime ExpirationDate { get; set; }
}
