namespace Domains.ApplicantDomain
{
    /// Model of passport
    public class Passport : IEntity
    {
        /// Unique identifier of <see cref="Passport"/>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// Number of <see cref="Passport"/>
        public string Number { get; set; }

        /// Issuing authority of <see cref="Passport"/>
        public string Issuer { get; set; }

        /// Date of issue
        public DateOnly IssueDate { get; set; }

        /// Date when the <see cref="Passport"/> expires
        public DateOnly ExpirationDate { get; set; }
    }
}
