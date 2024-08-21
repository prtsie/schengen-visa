using Domains.VisaApplicationDomain;

namespace ApplicationLayer.Services.VisaApplications.Requests;

/// Model of visa request from user
public record VisaApplicationCreateRequest(
    ReentryPermit ReentryPermit,
    string DestinationCountry,
    VisaCategory VisaCategory,
    bool IsForGroup,
    RequestedNumberOfEntries RequestedNumberOfEntries,
    int ValidDaysRequested,
    PastVisa[] PastVisas,
    PermissionToDestCountry? PermissionToDestCountry,
    PastVisit[] PastVisits
);
