using Domains.ApplicantDomain;

namespace ApplicationLayer.Services.Applicants.Models;

/// Model of
/// <see cref="Applicant" />
public class ApplicantModel
{
    /// <inheritdoc cref="Applicant.Name" />
    public Name Name { get; set; } = null!;

    /// <inheritdoc cref="Applicant.Passport" />
    public Passport Passport { get; set; } = null!;

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
    public Name FatherName { get; set; } = null!;

    /// <inheritdoc cref="Applicant.MotherName" />
    public Name MotherName { get; set; } = null!;

    /// <inheritdoc cref="Applicant.JobTitle" />
    public string JobTitle { get; set; } = null!;

    /// <inheritdoc cref="Applicant.PlaceOfWork" />
    public PlaceOfWork PlaceOfWork { get; set; } = null!;

    /// <inheritdoc cref="Applicant.IsNonResident" />
    public bool IsNonResident { get; set; }
}
