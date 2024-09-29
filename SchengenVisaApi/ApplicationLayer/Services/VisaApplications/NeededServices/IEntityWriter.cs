namespace ApplicationLayer.Services.VisaApplications.NeededServices
{
    public interface IEntityWriter
    {
        Task<Stream> WriteEntityToStream(object entity, CancellationToken cancellationToken);
    }
}
