namespace ApplicationLayer.Services.Locations.Requests
{
    public record AddCountryRequest(string CountryName, bool IsSchengen, string[] Cities);
}
