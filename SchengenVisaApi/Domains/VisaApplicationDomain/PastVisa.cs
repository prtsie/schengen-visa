using Domains.ApplicantDomain;

namespace Domains.VisaApplicationDomain
{
    /// Visa that <see cref="Applicant"/> already had
    public class PastVisa : IEntity
    {
        /// Unique identifier of <see cref="PastVisa"/>
        public Guid Id { get; private set; } = Guid.NewGuid();

        /// Date of issue
        public DateTime IssueDate { get; set; }

        /// Name of visa
        public string Name { get; set; } = null!;

        /// Date when visa expires
        public DateTime ExpirationDate { get; set; }
    }
}
