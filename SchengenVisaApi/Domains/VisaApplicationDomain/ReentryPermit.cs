namespace Domains.VisaApplicationDomain
{
    /// Permission to enter a country the issuer wants to come from
    public class ReentryPermit : IEntity
    {
        /// Unique identifier of <see cref="PastVisa"/>
        public Guid Id { get; private set; } = Guid.NewGuid();

        /// Date when <see cref="ReentryPermit"/> expires
        public DateOnly ExpirationDate { get; set; }
    }
}
