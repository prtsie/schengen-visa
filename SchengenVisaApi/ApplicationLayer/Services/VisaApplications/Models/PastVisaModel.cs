using System.ComponentModel.DataAnnotations;
using Domains;

namespace ApplicationLayer.Services.VisaApplications.Models;

/// Model of past visa for presentation layer
public class PastVisaModel
{
    // Date of issue
    [Required]
    public DateTime IssueDate { get; set; }

    /// Name of visa
    [MaxLength(ConfigurationConstraints.VisaNameLength)]
    public string Name { get; set; } = null!;

    /// Date when visa expires
    [Required]
    public DateTime ExpirationDate { get; set; }
}
