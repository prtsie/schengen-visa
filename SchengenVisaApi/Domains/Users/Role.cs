namespace Domains.Users;

/// Role of <see cref="User"/>
public enum Role
{
    /// Requests visa applications
    Applicant,
    /// Approves or declines applications
    ApprovingAuthority,
    /// Manages approving authorities
    Admin
}