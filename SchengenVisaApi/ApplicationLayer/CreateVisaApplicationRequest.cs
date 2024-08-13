using Domains.ApplicantDomain;
using Domains.VisaApplicationDomain;

namespace ApplicationLayer;

public record CreateVisaApplicationRequest(
    Name FullName,
    Passport Passport,
    DateOnly BirthDate,
    string BirthCity,
    string BirthCountry,
    string CitizenShip,
    string CitizenshipByBirth,
    Gender Gender,
    MaritalStatus MaritalStatus,
    Name FatherName,
    Name MotherName,
    bool IsNonResident,
    ReentryPermit ReentryPermit,
    string JobTitle,
    PlaceOfWork PlaceOfWork,
    string DestinationCountry,
    VisaCategory VisaCategory,
    bool IsForGroup,
    RequestedNumberOfEntries RequestedNumberOfEntries,
    int ValidDaysRequested,
    PastVisa[] PastVisas,
    PermissionToDestCountry? PermissionToDestCountry,
    PastVisit[] PastVisits
    );
