namespace ApplicationLayer.DataAccessingServices.VisaApplications.Models;

public class AddressModel
{
    /// City part of address
    public Guid CityId { get; set; }

    /// Street part of address
    public string Street { get; set; } = null!;

    /// Building part of address
    public string Building { get; set; } = null!;
}
