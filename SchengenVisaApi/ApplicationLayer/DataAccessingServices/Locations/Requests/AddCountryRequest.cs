namespace ApplicationLayer.DataAccessingServices.Locations.Requests
{
    public record AddCountryRequest(string CountryName, bool IsSchengen, string[] Cities);
}
