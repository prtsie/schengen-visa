using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using VisaApiClient;

namespace BlazorWebAssemblyVisaApiClient.Validation.Applicants.Models;

/// Model of request with attributes required for validation to work
public class RegisterApplicantRequestModel
{
    [Required]
    [ValidateComplexType]
    public RegisterRequestModel RegisterRequest { get; set; } = new();

    [Required]
    [ValidateComplexType]
    public NameModel ApplicantName { get; set; } = new();

    [Required]
    [ValidateComplexType]
    public PassportModel Passport { get; set; } = new();

    [Required(AllowEmptyStrings = true)]
    public DateTimeOffset BirthDate { get; set; }

    [Required]
    [StringLength(70, MinimumLength = 1)]
    public string CityOfBirth { get; set; } = default!;

    [Required]
    [StringLength(70, MinimumLength = 1)]
    public string CountryOfBirth { get; set; } = default!;

    [Required]
    [StringLength(30, MinimumLength = 1)]
    public string Citizenship { get; set; } = default!;

    [Required]
    [StringLength(30, MinimumLength = 1)]
    public string CitizenshipByBirth { get; set; } = default!;

    [Required(AllowEmptyStrings = true)]
    [JsonConverter(typeof(StringEnumConverter))]
    public Gender Gender { get; set; }

    [Required(AllowEmptyStrings = true)]
    [JsonConverter(typeof(StringEnumConverter))]
    public MaritalStatusModel MaritalStatus { get; set; }

    [Required]
    [ValidateComplexType]
    public NameModel FatherName { get; set; } = new();

    [Required]
    [ValidateComplexType]
    public NameModel MotherName { get; set; } = new();

    [Required]
    [StringLength(50, MinimumLength = 1)]
    public string JobTitle { get; set; } = default!;

    [Required]
    [ValidateComplexType]
    public PlaceOfWorkModel PlaceOfWork { get; set; } = new();

    public bool IsNonResident { get; set; }
}
