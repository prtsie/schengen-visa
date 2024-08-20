using Domains.VisaApplicationDomain;

namespace ApplicationLayer.Services.VisaApplications.Models
{
    /// Model of <see cref="VisaApplication"/>
    public class VisaApplicationModelForApplicant
    {

        /// <inheritdoc cref="VisaApplication.ReentryPermit"/>
        public ReentryPermit? ReentryPermit { get; set; }

        /// <inheritdoc cref="VisaApplication.DestinationCountry"/>
        public string DestinationCountry { get; set; } = null!;

        /// <inheritdoc cref="VisaApplication.PastVisas"/>
        public List<PastVisa> PastVisas { get; set; } = null!;

        /// <inheritdoc cref="VisaApplication.PermissionToDestCountry"/>
        public PermissionToDestCountry? PermissionToDestCountry { get; set; }

        public List<PastVisitModel> PastVisits { get; set; } = null!;

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
