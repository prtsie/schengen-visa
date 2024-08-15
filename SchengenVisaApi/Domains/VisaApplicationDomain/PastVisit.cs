using Domains.ApplicantDomain;

namespace Domains.VisaApplicationDomain
{
    /// Visit in a Schengen country that <see cref="Applicant"/> already had
    public class PastVisit : IEntity
    {
        /// Unique identifier of <see cref="PastVisit"/>
        public Guid Id { get; private set; } = Guid.NewGuid();

        /// First day of <see cref="PastVisit"/>
        public DateTime StartDate { get; set; }

        /// Last day of <see cref="PastVisit"/>
        public DateTime EndDate { get; set; }
    }
}
