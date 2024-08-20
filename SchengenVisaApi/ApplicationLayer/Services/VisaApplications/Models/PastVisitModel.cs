using Domains.VisaApplicationDomain;

namespace ApplicationLayer.Services.VisaApplications.Models
{
    /// Model of <see cref="PastVisit"/> with only name of the destination country
    public class PastVisitModel
    {
        /// <inheritdoc cref="PastVisit.StartDate"/>
        public DateTime StartDate { get; set; }

        /// <inheritdoc cref="PastVisit.EndDate"/>
        public DateTime EndDate { get; set; }

        /// <inheritdoc cref="PastVisit.DestinationCountry"/>
        public string DestinationCountry { get; set; } = null!;
    }
}
