using Domains.VisaApplicationDomain;

namespace ApplicationLayer.Services.VisaApplications.Models;

/// Model of <see cref="VisaApplication" />
public class VisaApplicationModelForApplicant
{
    /// <inheritdoc cref="VisaApplication.Id" />
    public Guid Id { get; set; }

    /// <inheritdoc cref="VisaApplication.Status" />
    public ApplicationStatus Status { get; set; }

    /// <inheritdoc cref="VisaApplication.ReentryPermit" />
    public ReentryPermitModel? ReentryPermit { get; set; }

    /// <inheritdoc cref="VisaApplication.DestinationCountry" />
    public string DestinationCountry { get; set; } = null!;

    /// <inheritdoc cref="VisaApplication.PastVisas" />
    public List<PastVisaModel> PastVisas { get; set; } = null!;

    /// <inheritdoc cref="VisaApplication.PermissionToDestCountry" />
    public PermissionToDestCountryModel? PermissionToDestCountry { get; set; }

    /// <inheritdoc cref="VisaApplication.PastVisits" />
    public List<PastVisitModel> PastVisits { get; set; } = null!;

    /// <inheritdoc cref="VisaApplication.VisaCategory" />
    public VisaCategory VisaCategory { get; set; }

    /// <inheritdoc cref="VisaApplication.ForGroup" />
    public bool ForGroup { get; set; }

    /// <inheritdoc cref="VisaApplication.RequestedNumberOfEntries" />
    public RequestedNumberOfEntries RequestedNumberOfEntries { get; set; }

    /// <inheritdoc cref="VisaApplication.RequestDate" />
    public DateTime RequestDate { get; set; }

    /// <inheritdoc cref="VisaApplication.ValidDaysRequested" />
    public int ValidDaysRequested { get; set; }
}
