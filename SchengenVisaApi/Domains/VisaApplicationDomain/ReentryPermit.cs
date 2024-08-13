namespace Domains.VisaApplicationDomain
{
    /// Permission to enter a country the issuer wants to come from
    /// <remarks>Owned</remarks>
    public class ReentryPermit
    {
        /// Number of <see cref="ReentryPermit"/>
        public string Number { get; set; } = null!;

        /// Date when <see cref="ReentryPermit"/> expires
        public DateOnly ExpirationDate { get; set; }
    }
}
