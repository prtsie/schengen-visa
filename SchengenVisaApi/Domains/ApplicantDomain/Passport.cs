namespace Domains.ApplicantDomain
{
    /// Model of passport
    /// <remarks>Owned</remarks>
    public class Passport
    {
        /// Number of <see cref="Passport"/>
        public string Number { get; set; } = null!;

        /// Issuing authority of <see cref="Passport"/>
        public string Issuer { get; set; } = null!;

        /// Date of issue
        public DateOnly IssueDate { get; set; }

        /// Date when the <see cref="Passport"/> expires
        public DateOnly ExpirationDate { get; set; }
    }
}
