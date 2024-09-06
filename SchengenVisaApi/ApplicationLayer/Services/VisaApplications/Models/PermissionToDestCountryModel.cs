using System.ComponentModel.DataAnnotations;
using Domains;

namespace ApplicationLayer.Services.VisaApplications.Models;

/// Model of permission to destination country for presentation layer
public class PermissionToDestCountryModel
{
    /// Date when permission to destination country expires
    public DateTime ExpirationDate { get; set; }

    /// Issuing authority
    [MaxLength(ConfigurationConstraints.IssuerNameLength)]
    public string Issuer { get; set; } = null!;
}
