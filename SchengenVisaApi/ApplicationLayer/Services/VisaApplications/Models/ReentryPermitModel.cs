﻿using System.ComponentModel.DataAnnotations;
using Domains;

namespace ApplicationLayer.Services.VisaApplications.Models;

/// Model of re-entry permit for presentation layer
public class ReentryPermitModel
{
    /// Number of re-entry permit
    [MaxLength(ConfigurationConstraints.ReentryPermitNumberLength)]
    public string Number { get; set; } = null!;

    /// Date when re-entry permit expires
    public DateTime ExpirationDate { get; set; }
}
