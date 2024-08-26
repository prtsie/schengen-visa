using ApplicationLayer.Services.Applicants.Models;
using ApplicationLayer.Services.AuthServices.Common;
using Domains.ApplicantDomain;

namespace ApplicationLayer.Services.AuthServices.Requests
{
    public record RegisterApplicantRequest(
        AuthData AuthData,
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
        PlaceOfWorkModel PlaceOfWork,
        bool IsNonResident) : RegisterRequest(AuthData);
}
