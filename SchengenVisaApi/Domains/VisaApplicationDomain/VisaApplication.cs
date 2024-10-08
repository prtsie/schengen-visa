﻿using Domains.ApplicantDomain;

namespace Domains.VisaApplicationDomain;

/// Model of visit request
public class VisaApplication : IEntity
{
    /// Unique identifier of <see cref="VisaApplication"/>
    public Guid Id { get; private set; } = Guid.NewGuid();

    /// Identifier of the <see cref="Applicant"/>
    public Guid ApplicantId { get; set; }

    /// Status of application
    public ApplicationStatus Status { get; set; }

    /// <inheritdoc cref="Domains.VisaApplicationDomain.ReentryPermit"/>
    /// <remarks>always null if <see cref="Applicant"/> is not a non-resident</remarks>
    public ReentryPermit? ReentryPermit { get; set; }

    /// Country that <see cref="Applicant"/> wants to visit
    public string DestinationCountry { get; set; } = null!;

    /// List of <see cref="PastVisa"/> that applicant had before
    public List<PastVisa> PastVisas { get; set; } = null!;

    /// Permission to enter the destination country of <see cref="Applicant"/>
    /// <remarks>always null if <see cref="DestinationCountry"/> is Schengen</remarks>
    public PermissionToDestCountry? PermissionToDestCountry { get; set; }

    public List<PastVisit> PastVisits { get; set; } = null!;

    /// <see cref="Domains.VisaApplicationDomain.VisaCategory"/>
    public VisaCategory VisaCategory { get; set; }

    /// Is for group
    public bool ForGroup { get; set; }

    /// <see cref="Domains.VisaApplicationDomain.RequestedNumberOfEntries"/>
    public RequestedNumberOfEntries RequestedNumberOfEntries { get; set; }

    /// When application was created
    public DateTime RequestDate { get; set; }

    /// Valid days requested
    public int ValidDaysRequested { get; set; }
}
