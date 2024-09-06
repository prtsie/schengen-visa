﻿using System.ComponentModel.DataAnnotations;
using Domains.VisaApplicationDomain;

namespace ApplicationLayer.Services.VisaApplications.Models;

/// Model of <see cref="VisaApplication" />
public class VisaApplicationModelForApplicant
{
    /// <inheritdoc cref="VisaApplication.Id" />
    [Required]
    public Guid Id { get; set; }

    /// <inheritdoc cref="VisaApplication.Status" />
    [Required]
    public ApplicationStatus Status { get; set; }

    /// <inheritdoc cref="VisaApplication.ReentryPermit" />
    public ReentryPermitModel? ReentryPermit { get; set; }

    /// <inheritdoc cref="VisaApplication.DestinationCountry" />
    [Required]
    public string DestinationCountry { get; set; } = null!;

    /// <inheritdoc cref="VisaApplication.PastVisas" />
    [Required]
    public List<PastVisaModel> PastVisas { get; set; } = null!;

    /// <inheritdoc cref="VisaApplication.PermissionToDestCountry" />
    public PermissionToDestCountryModel? PermissionToDestCountry { get; set; }

    /// <inheritdoc cref="VisaApplication.PastVisits" />
    [Required]
    public List<PastVisitModel> PastVisits { get; set; } = null!;

    /// <inheritdoc cref="VisaApplication.VisaCategory" />
    [Required]
    public VisaCategory VisaCategory { get; set; }

    /// <inheritdoc cref="VisaApplication.ForGroup" />
    [Required]
    public bool ForGroup { get; set; }

    /// <inheritdoc cref="VisaApplication.RequestedNumberOfEntries" />
    [Required]
    public RequestedNumberOfEntries RequestedNumberOfEntries { get; set; }

    /// <inheritdoc cref="VisaApplication.RequestDate" />
    [Required]
    public DateTime RequestDate { get; set; }

    /// <inheritdoc cref="VisaApplication.ValidDaysRequested" />
    [Required]
    public int ValidDaysRequested { get; set; }
}
