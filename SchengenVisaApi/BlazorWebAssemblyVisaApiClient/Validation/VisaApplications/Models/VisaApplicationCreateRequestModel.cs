using System.ComponentModel.DataAnnotations;
using VisaApiClient;

namespace BlazorWebAssemblyVisaApiClient.Validation.VisaApplications.Models
{
    /// Model for request for data annotations validation to work
    public class VisaApplicationCreateRequestModel
    {
        [ValidateComplexType]
        public ReentryPermitModel? ReentryPermit { get; set; } = new();

        [Required]
        [MaxLength(ConfigurationConstraints.CountryNameLength)]
        public string DestinationCountry { get; set; } = default!;

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
        [ValidateComplexType]
        public PastVisaModel[] PastVisas { get; set; } = default!;

        [ValidateComplexType]
        public PermissionToDestCountryModel? PermissionToDestCountry { get; set; } = new();

        [Required]
        [ValidateComplexType]
        public PastVisitModel[] PastVisits { get; set; } = default!;
    }
}
