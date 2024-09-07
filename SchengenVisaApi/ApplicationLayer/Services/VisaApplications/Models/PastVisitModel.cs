using System.ComponentModel.DataAnnotations;
using Domains;

namespace ApplicationLayer.Services.VisaApplications.Models;

/// Model of past visit for presentation layer
public class PastVisitModel
{
    /// First day of past visit
    [Required]
    public DateTime StartDate { get; set; }

    /// Last day of past visit
    [Required]
    public DateTime EndDate { get; set; }

    /// Destination country of past visit
    [MaxLength(ConfigurationConstraints.CountryNameLength)]
    public string DestinationCountry { get; set; } = null!;
}
