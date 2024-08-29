using System.ComponentModel.DataAnnotations;
using ApplicationLayer.Services.Applicants.Models;
using Domains;
using Domains.ApplicantDomain;

namespace ApplicationLayer.Services.AuthServices.Requests;

public record RegisterApplicantRequest
{
    [Required] public RegisterRequest RegisterRequest { get; set; } = null!;

    [Required] public NameModel ApplicantName { get; set; } = null!;

    [Required] public PassportModel Passport { get; set; } = null!;

    [Required] public DateTime BirthDate { get; set; }

    [Required]
    [MaxLength(ConfigurationConstraints.CityNameLength)]
    public string CityOfBirth { get; set; } = null!;

    [Required]
    [MaxLength(ConfigurationConstraints.CountryNameLength)]
    public string CountryOfBirth { get; set; } = null!;

    [Required]
    [MaxLength(ConfigurationConstraints.CitizenshipLength)]
    public string Citizenship { get; set; } = null!;

    [Required]
    [MaxLength(ConfigurationConstraints.CitizenshipLength)]
    public string CitizenshipByBirth { get; set; } = null!;

    [Required] public Gender Gender { get; set; }

    [Required] public MaritalStatus MaritalStatus { get; set; }

    [Required] public NameModel FatherName { get; set; } = null!;

    [Required] public NameModel MotherName { get; set; } = null!;

    [Required]
    [MaxLength(ConfigurationConstraints.JobTitleLength)]
    public string JobTitle { get; set; } = null!;

    [Required] public PlaceOfWorkModel PlaceOfWork { get; set; } = null!;

    [Required] public bool IsNonResident { get; set; }
}
