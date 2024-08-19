using ApplicationLayer.Services.Applicants.Models;
using Domains.ApplicantDomain;

namespace ApplicationLayer.Services.AuthServices.Requests
{
    public record RegisterApplicantRequest(
        string Email,
        string Password,
        Name ApplicantName,
        Passport Passport,
        DateTime BirthDate,
        Guid CityOfBirthId,
        string Citizenship,
        string CitizenshipByBirth,
        Gender Gender,
        MaritalStatus MaritalStatus,
        Name FatherName,
        Name MotherName,
        string JobTitle,
        PlaceOfWorkModel PlaceOfWork,
        bool IsNonResident);
}
