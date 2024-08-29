using Domains.ApplicantDomain;

namespace ApplicationLayer.Services.Applicants.Models;

/// Model of <see cref="Applicant" />
public class ApplicantModel
{
    /// <inheritdoc cref="Applicant.Name" />
    public NameModel Name { get; set; } = null!;

    /// <inheritdoc cref="Applicant.Passport" />
    public PassportModel Passport { get; set; } = null!;

    /// <inheritdoc cref="Applicant.BirthDate" />
    public DateTime BirthDate { get; set; }

    /// <inheritdoc cref="Applicant.CountryOfBirth" />
    public string CountryOfBirth { get; set; } = null!;

    /// <inheritdoc cref="Applicant.CityOfBirth" />
    public string CityOfBirth { get; set; } = null!;

    /// <inheritdoc cref="Applicant.Citizenship" />
    public string Citizenship { get; set; } = null!;

    /// <inheritdoc cref="Applicant.CitizenshipByBirth" />
    public string CitizenshipByBirth { get; set; } = null!;

    /// <inheritdoc cref="Applicant.Gender" />
    public Gender Gender { get; set; }

    /// <inheritdoc cref="Applicant.MaritalStatus" />
    public MaritalStatus MaritalStatus { get; set; }

    /// <inheritdoc cref="Applicant.FatherName" />
    public NameModel FatherName { get; set; } = null!;

    /// <inheritdoc cref="Applicant.MotherName" />
    public NameModel MotherName { get; set; } = null!;

    /// <inheritdoc cref="Applicant.JobTitle" />
    public string JobTitle { get; set; } = null!;

    /// <inheritdoc cref="Applicant.PlaceOfWork" />
    public PlaceOfWorkModel PlaceOfWork { get; set; } = null!;

    /// <inheritdoc cref="Applicant.IsNonResident" />
    public bool IsNonResident { get; set; }
}
