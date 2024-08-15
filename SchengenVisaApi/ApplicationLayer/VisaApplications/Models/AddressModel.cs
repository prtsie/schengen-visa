namespace ApplicationLayer.VisaApplications.Models;

public class AddressModel
{
    /// Country part of address
    public string Country { get; set; } = null!;

    /// City part of address
    public string City { get; set; } = null!;

    /// Street part of address
    public string Street { get; set; } = null!;

    /// Building part of address
    public string Building { get; set; } = null!;
}
