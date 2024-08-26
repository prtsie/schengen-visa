namespace Domains.VisaApplicationDomain;

public enum ApplicationStatus
{
    /// Waits for approve
    Pending,
    Approved,
    Rejected,
    /// Closed by applicant
    Closed
}