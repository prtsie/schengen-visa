namespace ApplicationLayer.DataAccessingServices.VisaApplications.Models
{
    public class PastVisitModel
    {
        /// First day of <see cref="PastVisitModel"/>
        public DateTime StartDate { get; set; }

        /// Last day of <see cref="PastVisitModel"/>
        public DateTime EndDate { get; set; }

        /// Identifier of destination country of <see cref="PastVisitModel"/>
        public Guid DestinationCountryId { get; set; }
    }
}
