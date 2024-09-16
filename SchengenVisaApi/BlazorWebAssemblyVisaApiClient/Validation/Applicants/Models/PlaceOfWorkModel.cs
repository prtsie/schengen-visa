using System.ComponentModel.DataAnnotations;
using VisaApiClient;

namespace BlazorWebAssemblyVisaApiClient.Validation.Applicants.Models
{
    /// Model of place of work with attributes required for validation to work
    public class PlaceOfWorkModel
    {
        [Required]
        [StringLength(ConfigurationConstraints.PlaceOfWorkNameLength, MinimumLength = 1)]
        public string Name { get; set; } = default!;

        [Required]
        [ValidateComplexType]
        public AddressModel Address { get; set; } = new AddressModel();

        [Required]
        [StringLength(ConfigurationConstraints.PhoneNumberLength, MinimumLength = ConfigurationConstraints.PhoneNumberMinLength)]
        public string PhoneNum { get; set; } = default!;
    }
}
