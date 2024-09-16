using System.ComponentModel.DataAnnotations;
using VisaApiClient;

namespace BlazorWebAssemblyVisaApiClient.Validation.VisaApplications.Models
{
    /// Model for request for data annotations validation to work
    public class VisaApplicationCreateRequestModel
    {
        [ValidateComplexType]
        public ReentryPermitModel? ReentryPermit { get; set; } = default!;

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

        [ValidateComplexType]
        public List<PastVisaModel> PastVisas { get; set; } = [];

        [ValidateComplexType]
        public PermissionToDestCountryModel? PermissionToDestCountry { get; set; } = default!;

        [ValidateComplexType]
        public List<PastVisitModel> PastVisits { get; set; } = [];
    }
}
