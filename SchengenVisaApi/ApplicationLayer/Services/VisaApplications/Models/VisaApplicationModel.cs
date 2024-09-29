using System.ComponentModel.DataAnnotations;
using System.Text;
using ApplicationLayer.Services.Applicants.Models;
using Domains.VisaApplicationDomain;

namespace ApplicationLayer.Services.VisaApplications.Models;

/// Model of <see cref="VisaApplication" /> with applicant property
public class VisaApplicationModel
{
    /// <inheritdoc cref="VisaApplication.Id" />
    [Required]
    public Guid Id { get; set; }

    /// Applicant of application
    [Required]
    public ApplicantModel Applicant { get; set; } = null!;

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

    [Required] public List<PastVisitModel> PastVisits { get; set; } = null!;

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

    public string ForGroupToString() => ForGroup ? "For group" : "Individual";

    public string PastVisasToString()
    {
        var stringBuilder = new StringBuilder();
        foreach (var visa in PastVisas)
        {
            stringBuilder.AppendLine($"{visa.Name} issued at {visa.IssueDate.ToShortDateString()} and valid for {visa.ExpirationDate.ToShortDateString()}");
        }

        return stringBuilder.ToString();
    }

    public string PastVisitsToString()
    {
        var stringBuilder = new StringBuilder();
        foreach (var visit in PastVisits)
        {
            stringBuilder.AppendLine($"Visit to {visit.DestinationCountry} started at {visit.StartDate.ToShortDateString()} and ends {visit.EndDate.ToShortDateString()}");
        }

        return stringBuilder.ToString();
    }

    public string PermissionToDestCountryToString()
    {
        return VisaCategory is VisaCategory.Transit
            ? $"Issued by{PermissionToDestCountry!.Issuer}, expires at {PermissionToDestCountry.ExpirationDate.ToShortDateString()}"
            : "Non-transit";
    }
}
