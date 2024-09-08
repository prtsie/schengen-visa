using System.ComponentModel.DataAnnotations;
using Domains.VisaApplicationDomain;

namespace ApplicationLayer.Services.VisaApplications.Models;

/// Model of <see cref="VisaApplication" />
public class VisaApplicationPreview
{
    /// <inheritdoc cref="VisaApplication.Id" />
    [Required]
    public Guid Id { get; set; }

    /// <inheritdoc cref="VisaApplication.Status" />
    [Required]
    public ApplicationStatus Status { get; set; }

    /// <inheritdoc cref="VisaApplication.DestinationCountry" />
    [Required]
    public string DestinationCountry { get; set; } = null!;

    /// <inheritdoc cref="VisaApplication.VisaCategory" />
    [Required]
    public VisaCategory VisaCategory { get; set; }

    /// <inheritdoc cref="VisaApplication.RequestDate" />
    [Required]
    public DateTime RequestDate { get; set; }

    /// <inheritdoc cref="VisaApplication.ValidDaysRequested" />
    [Required]
    public int ValidDaysRequested { get; set; }
}
