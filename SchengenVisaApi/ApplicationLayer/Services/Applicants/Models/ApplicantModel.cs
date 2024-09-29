using System.ComponentModel.DataAnnotations;
using Domains.ApplicantDomain;

namespace ApplicationLayer.Services.Applicants.Models;

/// Model of <see cref="Applicant" />
public class ApplicantModel
{
    /// <inheritdoc cref="Applicant.Name" />
    [Required]
    public NameModel Name { get; set; } = null!;

    /// <inheritdoc cref="Applicant.Passport" />
    [Required]
    public PassportModel Passport { get; set; } = null!;

    /// <inheritdoc cref="Applicant.BirthDate" />
    [Required]
    public DateTime BirthDate { get; set; }

    /// <inheritdoc cref="Applicant.CountryOfBirth" />
    [Required]
    public string CountryOfBirth { get; set; } = null!;

    /// <inheritdoc cref="Applicant.CityOfBirth" />
    [Required]
    public string CityOfBirth { get; set; } = null!;

    /// <inheritdoc cref="Applicant.Citizenship" />
    [Required]
    public string Citizenship { get; set; } = null!;

    /// <inheritdoc cref="Applicant.CitizenshipByBirth" />
    [Required]
    public string CitizenshipByBirth { get; set; } = null!;

    /// <inheritdoc cref="Applicant.Gender" />
    [Required]
    public Gender Gender { get; set; }

    /// <inheritdoc cref="Applicant.MaritalStatus" />
    [Required]
    public MaritalStatus MaritalStatus { get; set; }

    /// <inheritdoc cref="Applicant.FatherName" />
    [Required]
    public NameModel FatherName { get; set; } = null!;

    /// <inheritdoc cref="Applicant.MotherName" />
    [Required]
    public NameModel MotherName { get; set; } = null!;

    /// <inheritdoc cref="Applicant.JobTitle" />
    [Required]
    public string JobTitle { get; set; } = null!;

    /// <inheritdoc cref="Applicant.PlaceOfWork" />
    [Required]
    public PlaceOfWorkModel PlaceOfWork { get; set; } = null!;

    /// <inheritdoc cref="Applicant.IsNonResident" />
    [Required]
    public bool IsNonResident { get; set; }

    public override string ToString() => Name.ToString();
}
