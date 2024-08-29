using System.ComponentModel.DataAnnotations;
using Domains;

namespace ApplicationLayer.Services.Applicants.Models;

public class AddressModel
{
    /// Country part of address
    [Required]
    [MaxLength(ConfigurationConstraints.CountryNameLength)]
    public string Country { get; set; } = null!;

    /// City part of address
    [Required]
    [MaxLength(ConfigurationConstraints.CityNameLength)]
    public string City { get; set; } = null!;

    /// Street part of address
    [Required]
    [MaxLength(ConfigurationConstraints.StreetNameLength)]
    public string Street { get; set; } = null!;

    /// Building part of address
    [Required]
    [MaxLength(ConfigurationConstraints.BuildingNumberLength)]
    public string Building { get; set; } = null!;
}
