namespace ApplicationLayer.DataAccessingServices.VisaApplications.Models;

public class PlaceOfWorkModel
{
    /// Name of hirer
    public string Name { get; set; } = null!;

    /// <see cref="AddressModel"/> of hirer
    public AddressModel Address { get; set; } = null!;

    /// Phone number of hirer
    public string PhoneNum { get; set; } = null!;
}
