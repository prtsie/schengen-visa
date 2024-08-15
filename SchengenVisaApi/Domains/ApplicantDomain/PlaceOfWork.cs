namespace Domains.ApplicantDomain;

public class PlaceOfWork : IEntity
{
    /// Unique identifier of <see cref="PlaceOfWork"/>
    public Guid Id { get; private set; } = Guid.NewGuid();

    /// Name of hirer
    public string Name { get; set; } = null!;

    /// <see cref="ApplicantDomain.Address"/> of hirer
    public Address Address { get; set; } = null!;

    /// Phone number of hirer
    public string PhoneNum { get; set; } = null!;
}