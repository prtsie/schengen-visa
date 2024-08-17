using ApplicationLayer.AuthServices.NeededServices;
using ApplicationLayer.AuthServices.RegisterService.Exceptions;
using ApplicationLayer.AuthServices.Requests;
using Domains.Users;

namespace ApplicationLayer.AuthServices.RegisterService
{
    /// <inheritdoc cref="IRegisterService"/>
    public class RegisterService(IUsersRepository users) : IRegisterService
    {
        async Task IRegisterService.Register(RegisterApplicantRequest request, CancellationToken cancellationToken)
        {
            if (await users.FindByEmailAsync(request.Email, cancellationToken) is not null)
            {
                throw new UserAlreadyExistsException(request);
            }

            //TODO mapper
            var user = new User
            {
                Email = request.Email,
                Password = request.Password,
                Role = Role.Applicant
            };

            await users.AddAsync(user, cancellationToken);
            await users.SaveAsync(cancellationToken);
            users.GetAllAsync(cancellationToken);
        }
    }
}
