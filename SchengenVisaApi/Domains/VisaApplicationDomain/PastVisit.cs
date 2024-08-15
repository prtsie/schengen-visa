using Domains.ApplicantDomain;

namespace Domains.VisaApplicationDomain;

/// Visit in a Schengen country that <see cref="Applicant"/> already had
/// <remarks>Owned</remarks>
public class PastVisit
{
    /// First day of <see cref="PastVisit"/>
    public DateTime StartDate { get; set; }

    /// Last day of <see cref="PastVisit"/>
    public DateTime EndDate { get; set; }
}
