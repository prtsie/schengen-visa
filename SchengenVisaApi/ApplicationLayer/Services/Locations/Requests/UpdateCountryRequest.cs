namespace ApplicationLayer.Services.Locations.Requests
{
    public record UpdateCountryRequest(Guid Id, string CountryName, bool IsSchengen, string[] Cities);
}
