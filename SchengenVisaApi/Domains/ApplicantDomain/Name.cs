namespace Domains.ApplicantDomain
{
    /// Model of full name
    /// <remarks>Owned</remarks>
    public class Name
    {
        public string FirstName { get; set; } = null!;

        public string Surname { get; set; } = null!;

        public string? Patronymic { get; set; }
    }
}
