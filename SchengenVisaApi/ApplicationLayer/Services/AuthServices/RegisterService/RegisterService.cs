using ApplicationLayer.InfrastructureServicesInterfaces;
using ApplicationLayer.Services.Applicants.NeededServices;
using ApplicationLayer.Services.AuthServices.NeededServices;
using ApplicationLayer.Services.AuthServices.Requests;
using AutoMapper;
using Domains.ApplicantDomain;
using Domains.Users;

namespace ApplicationLayer.Services.AuthServices.RegisterService;

/// <inheritdoc cref="IRegisterService"/>
public class RegisterService(
    IUsersRepository users,
    IApplicantsRepository applicants,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRegisterService
{
    async Task IRegisterService.RegisterApplicant(RegisterApplicantRequest request, CancellationToken cancellationToken)
    {
        var user = mapper.Map<User>(request.AuthData);
        user.Role = Role.Applicant;

        var applicant = mapper.Map<Applicant>(request);
        applicant.UserId = user.Id;

        await users.AddAsync(user, cancellationToken);
        await applicants.AddAsync(applicant, cancellationToken);

        await unitOfWork.SaveAsync(cancellationToken);
    }

    async Task IRegisterService.RegisterAuthority(RegisterRequest request, CancellationToken cancellationToken)
    {
        var user = mapper.Map<User>(request.AuthData);
        user.Role = Role.ApprovingAuthority;

        await users.AddAsync(user, cancellationToken);

        await unitOfWork.SaveAsync(cancellationToken);
    }
}