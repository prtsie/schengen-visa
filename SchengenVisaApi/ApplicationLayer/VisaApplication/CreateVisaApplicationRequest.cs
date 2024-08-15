using ApplicationLayer.VisaApplication.Models;
using Domains.ApplicantDomain;
using Domains.VisaApplicationDomain;

namespace ApplicationLayer.VisaApplication;

/// Model of visa request from user
public record CreateVisaApplicationRequest(
    Name FullName,
    Passport Passport,
    DateTime BirthDate,
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
    PlaceOfWorkModel PlaceOfWork,
    string DestinationCountry,
    VisaCategory VisaCategory,
    bool IsForGroup,
    RequestedNumberOfEntries RequestedNumberOfEntries,
    int ValidDaysRequested,
    PastVisa[] PastVisas,
    PermissionToDestCountry? PermissionToDestCountry,
    PastVisit[] PastVisits
);
