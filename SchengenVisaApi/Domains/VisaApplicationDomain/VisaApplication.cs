﻿using Domains.ApplicantDomain;
using Domains.Common;

namespace Domains.VisaApplicationDomain
{
    /// Model of visit request
    public class VisaApplication : IEntity
    {
        /// Unique identifier of <see cref="VisaApplication"/>
        public Guid Id { get; private set; } = Guid.NewGuid();

        /// Applicant of <see cref="VisaApplication"/>
        public Applicant Applicant { get; set; } = null!;

        /// <inheritdoc cref="Domains.VisaApplicationDomain.ReentryPermit"/>
        /// <remarks>always null if <see cref="Applicant"/> is not a non-resident</remarks>
        public ReentryPermit? ReentryPermit { get; set; }

        /// <see cref="Country"/> that <see cref="Applicant"/> wants to visit
        public Country DestinationCountry { get; set; } = null!;

        /// <summary>
        /// List of <see cref="PastVisa"/> that applicant had before
        /// </summary>
        public List<PastVisa> PastVisas { get; set; } = null!;

        /// Permission to enter the destination country of <see cref="Applicant"/>
        /// <remarks>always null if <see cref="DestinationCountry"/> is Schengen</remarks>
        public PermissionToDestCountry? PermissionToDestCountry { get; set; }

        public List<PastVisit> PastVisits { get; set; } = null!;
    }
}
