using Domains.VisaApplicationDomain;

namespace ApplicationLayer.Services.VisaApplications.Models
{
    /// Model of <see cref="PastVisit"/> with only identifier of country
    public class PastVisitModelForRequest
    {
        /// First day of <see cref="PastVisitModelForRequest"/>
        public DateTime StartDate { get; set; }

        /// Last day of <see cref="PastVisitModelForRequest"/>
        public DateTime EndDate { get; set; }

        /// Identifier of destination country of <see cref="PastVisitModelForRequest"/>
        public Guid DestinationCountryId { get; set; }
    }
}
