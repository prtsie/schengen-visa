using Domains.LocationDomain;
using Domains.VisaApplicationDomain;

namespace Domains.ApplicantDomain;

/// Model of an applicant
public class Applicant : IEntity
{
    /// Unique identifier of the <see cref="Applicant"/>
    public Guid Id { get; set; }

    /// Full name of the <see cref="Applicant"/>
    public Name Name { get; set; } = null!;

    /// Passport of <see cref="Applicant"/>
    public Passport Passport { get; set; } = null!;

    /// Date of birth of the <see cref="Applicant"/>
    public DateTime BirthDate { get; set; }

    /// <see cref="Country"/> of birth of the <see cref="Applicant"/>
    public Country CountryOfBirth { get; set; } = null!;

    /// <see cref="City"/> of birth of the <see cref="Applicant"/>
    public City CityOfBirth { get; set; } = null!;

    /// Citizenship of <see cref="Applicant"/>
    public string Citizenship { get; set; } = null!;

    /// Citizenship by birth of <see cref="Applicant"/>
    public string CitizenshipByBirth { get; set; } = null!;

    /// Gender of <see cref="Applicant"/>
    public Gender Gender { get; set; }

    /// Marital status of <see cref="Applicant"/>
    public MaritalStatus MaritalStatus { get; set; }

    /// Full name of the <see cref="Applicant"/>'s father
    public Name FatherName { get; set; } = null!;

    /// Full name of the <see cref="Applicant"/>'s mother
    public Name MotherName { get; set; } = null!;

    /// Position of <see cref="Applicant"/>
    public string JobTitle { get; set; } = null!;

    /// Place of <see cref="Applicant"/>'s work
    public PlaceOfWork PlaceOfWork { get; set; } = null!;

    /// Is <see cref="Applicant"/> a non-resident
    public bool IsNonResident { get; set; }

    /// List of <see cref="Applicant"/>'s applications
    public List<VisaApplication> VisaApplications { get; set; } = null!;
}