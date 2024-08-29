using System.ComponentModel.DataAnnotations;
using Domains;

namespace ApplicationLayer.Services.Applicants.Models;

public class PlaceOfWorkModel
{
    /// Name of hirer
    [Required]
    [MaxLength(ConfigurationConstraints.PlaceOfWorkNameLength)]
    public string Name { get; set; } = null!;

    /// Address of hirer
    [Required]
    public AddressModel Address { get; set; } = null!;

    /// Phone number of hirer
    [Required]
    [MaxLength(ConfigurationConstraints.PhoneNumberLength)]
    [MinLength(ConfigurationConstraints.PhoneNumberMinLength)]
    public string PhoneNum { get; set; } = null!;
}