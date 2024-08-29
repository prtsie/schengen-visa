using ApplicationLayer.Services.Users.Models;
using ApplicationLayer.Services.Users.Requests;

namespace ApplicationLayer.Services.Users;

/// user accounts service
public interface IUsersService
{
    /// Returns all user accounts with role of approving authority
    /// <param name="cancellationToken">Cancellation token</param>
    Task<List<UserModel>> GetAuthoritiesAccountsAsync(CancellationToken cancellationToken);

    /// Changes authentication data for an authority account
    /// <param name="request"> Request object with identifier of user and new authentication data</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task ChangeAuthorityAuthDataAsync(ChangeUserAuthDataRequest request, CancellationToken cancellationToken);

    /// Removes account of authority
    /// <param name="userId">Identifier of account</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task RemoveAuthorityAccount(Guid userId, CancellationToken cancellationToken);
}
