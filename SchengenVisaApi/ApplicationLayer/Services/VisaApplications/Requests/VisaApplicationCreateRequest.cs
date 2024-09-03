using System.ComponentModel.DataAnnotations;
using ApplicationLayer.Services.VisaApplications.Models;
using Domains;
using Domains.VisaApplicationDomain;

namespace ApplicationLayer.Services.VisaApplications.Requests;

/// Model of visa request from user
public class VisaApplicationCreateRequest
{

    [Required]
    public ReentryPermitModel? ReentryPermit { get; set; }

    [Required]
    [MaxLength(ConfigurationConstraints.CountryNameLength)]
    public string DestinationCountry { get; set; } = null!;

    [Required]
    public VisaCategory VisaCategory { get; set; }

    [Required]
    public bool IsForGroup { get; set; }

    [Required]
    public RequestedNumberOfEntries RequestedNumberOfEntries { get; set; }

    [Required]
    [Range(0, ConfigurationConstraints.MaxValidDays)]
    public int ValidDaysRequested { get; set; }

    [Required]
    public PastVisaModel[] PastVisas { get; set; } = null!;

    //todo remove attribute
    [Required]
    public PermissionToDestCountryModel? PermissionToDestCountry { get; set; }

    [Required]
    public PastVisitModel[] PastVisits { get; set; } = null!;
}
