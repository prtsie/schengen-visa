namespace ApplicationLayer.InfrastructureServicesInterfaces;

public interface IUserIdProvider
{
    /// Returns identifier of authenticated user who sent the request
    Guid GetUserId();
}