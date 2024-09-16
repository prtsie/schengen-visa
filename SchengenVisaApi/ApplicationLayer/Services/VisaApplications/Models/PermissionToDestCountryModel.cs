﻿using System.ComponentModel.DataAnnotations;
using Domains;

namespace ApplicationLayer.Services.VisaApplications.Models;

/// Model of permission to destination country for presentation layer
public class PermissionToDestCountryModel
{
    /// Date when permission to destination country expires
    [Required]
    public DateTime ExpirationDate { get; set; }

    /// Issuing authority
    [MaxLength(ConfigurationConstraints.IssuerNameLength)]
    [Required]
    public string Issuer { get; set; } = null!;
}
