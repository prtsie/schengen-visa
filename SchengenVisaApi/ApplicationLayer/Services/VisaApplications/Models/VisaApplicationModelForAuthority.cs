using ApplicationLayer.Services.Applicants.Models;
using Domains.VisaApplicationDomain;

namespace ApplicationLayer.Services.VisaApplications.Models
{
    /// Model of <see cref="VisaApplication"/> with applicant property
    public class VisaApplicationModelForAuthority
    {
        /// <inheritdoc cref="VisaApplication.Id"/>
        public Guid Id { get; set; }

        /// Applicant of application
        public ApplicantModel Applicant { get; set; } = null!;

        /// <inheritdoc cref="VisaApplication.Status"/>
        public ApplicationStatus Status { get; set; }

        /// <inheritdoc cref="VisaApplication.ReentryPermit"/>
        public ReentryPermit? ReentryPermit { get; set; }

        /// <inheritdoc cref="VisaApplication.DestinationCountry"/>
        public string DestinationCountry { get; set; } = null!;

        /// <inheritdoc cref="VisaApplication.PastVisas"/>
        public List<PastVisa> PastVisas { get; set; } = null!;

        /// <inheritdoc cref="VisaApplication.PermissionToDestCountry"/>
        public PermissionToDestCountry? PermissionToDestCountry { get; set; }

        public List<PastVisit> PastVisits { get; set; } = null!;

        /// <inheritdoc cref="VisaApplication.VisaCategory"/>
        public VisaCategory VisaCategory { get; set; }

        /// <inheritdoc cref="VisaApplication.ForGroup"/>
        public bool ForGroup { get; set; }

        /// <inheritdoc cref="VisaApplication.RequestedNumberOfEntries"/>
        public RequestedNumberOfEntries RequestedNumberOfEntries { get; set; }

        /// <inheritdoc cref="VisaApplication.RequestDate"/>
        public DateTime RequestDate { get; set; }

        /// <inheritdoc cref="VisaApplication.ValidDaysRequested"/>
        public int ValidDaysRequested { get; set; }
    }
}
