using Domains.ApplicantDomain;

namespace Domains.VisaApplicationDomain;

/// Visa that <see cref="Applicant"/> already had
/// <remarks>Owned</remarks>
public class PastVisa
{
    /// Date of issue
    public DateTime IssueDate { get; set; }

    /// Name of visa
    public string Name { get; set; } = null!;

    /// Date when visa expires
    public DateTime ExpirationDate { get; set; }
}
