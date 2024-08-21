using ApplicationLayer.Services.AuthServices.Requests;
using Domains.Users;

namespace ApplicationLayer.Services.ApprovingAuthorities
{
    /// user accounts service
    public interface IUsersService
    {
        /// Returns all user accounts with role of approving authority
        /// <param name="cancellationToken">Cancellation token</param>
        Task<List<User>> GetAuthoritiesAccountsAsync(CancellationToken cancellationToken);

        /// Changes authentication data for an account
        /// <param name="userId">identifier of account</param>
        /// <param name="data">request data with new email and password</param>
        /// <param name="cancellationToken">Cancellation token</param>
        Task ChangeAccountAuthDataAsync(Guid userId, RegisterRequest data, CancellationToken cancellationToken);

        /// Removes user account
        /// <param name="userId">Identifier of account</param>
        /// <param name="cancellationToken">Cancellation token</param>
        Task RemoveUserAccount(Guid userId, CancellationToken cancellationToken);
    }
}
