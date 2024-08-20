﻿using ApplicationLayer.Services.VisaApplications.Models;
using Domains.VisaApplicationDomain;

namespace ApplicationLayer.Services.VisaApplications.Requests;

/// Model of visa request from user
public record VisaApplicationCreateRequest(
    ReentryPermit ReentryPermit,
    Guid DestinationCountryId,
    VisaCategory VisaCategory,
    bool IsForGroup,
    RequestedNumberOfEntries RequestedNumberOfEntries,
    int ValidDaysRequested,
    PastVisa[] PastVisas,
    PermissionToDestCountry? PermissionToDestCountry,
    PastVisitModelForRequest[] PastVisits
);
