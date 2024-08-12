namespace Domains.VisaApplicationDomain
{
    /// Permission to enter the destination country
    /// <remarks>Owned</remarks>
    public class PermissionToDestCountry
    {
        /// Date when <see cref="PermissionToDestCountry"/> expires
        public DateOnly ExpirationDate { get; set; }

        /// Issuing authority
        public string Issuer { get; set; } = null!;
    }
}
