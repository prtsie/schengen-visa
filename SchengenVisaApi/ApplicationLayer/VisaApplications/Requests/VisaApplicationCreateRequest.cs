﻿using ApplicationLayer.VisaApplications.Models;
using Domains.ApplicantDomain;
using Domains.VisaApplicationDomain;

namespace ApplicationLayer.VisaApplications.Requests;

/// Model of visa request from user
public record VisaApplicationCreateRequest(
    Name FullName,
    Passport Passport,
    DateTime BirthDate,
    Guid BirthCityId,
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
    Guid DestinationCountryId,
    VisaCategory VisaCategory,
    bool IsForGroup,
    RequestedNumberOfEntries RequestedNumberOfEntries,
    int ValidDaysRequested,
    PastVisa[] PastVisas,
    PermissionToDestCountry? PermissionToDestCountry,
    PastVisitModel[] PastVisits
);
