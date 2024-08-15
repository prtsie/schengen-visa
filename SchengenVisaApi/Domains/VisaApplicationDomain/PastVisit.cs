using Domains.ApplicantDomain;
using Domains.LocationDomain;

namespace Domains.VisaApplicationDomain;

/// Visit in a Schengen country that <see cref="Applicant"/> already had
/// <remarks>Owned</remarks>
public class PastVisit
{
    /// First day of <see cref="PastVisit"/>
    public DateTime StartDate { get; set; }

    /// Last day of <see cref="PastVisit"/>
    public DateTime EndDate { get; set; }

    /// Destination country of <see cref="PastVisit"/>
    public Country DestinationCountry { get; set; } = null!;
}
