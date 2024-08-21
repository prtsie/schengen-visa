using Domains.ApplicantDomain;

namespace ApplicationLayer.Services.AuthServices.Requests
{
    public record RegisterApplicantRequest(
        string Email,
        string Password,
        Name ApplicantName,
        Passport Passport,
        DateTime BirthDate,
        string CityOfBirth,
        string CountryOfBirth,
        string Citizenship,
        string CitizenshipByBirth,
        Gender Gender,
        MaritalStatus MaritalStatus,
        Name FatherName,
        Name MotherName,
        string JobTitle,
        PlaceOfWork PlaceOfWork,
        bool IsNonResident) : RegisterRequest(Email, Password);
}
