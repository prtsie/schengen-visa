namespace Infrastructure.Database
{
    public interface IUnitOfWork
    {
        /// Save changes in data storage
        /// <param name="cancellationToken">Cancellation Token</param>
        Task SaveAsync(CancellationToken cancellationToken);
    }
}
