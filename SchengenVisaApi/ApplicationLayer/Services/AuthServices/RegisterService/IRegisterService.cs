using ApplicationLayer.Services.AuthServices.Requests;

namespace ApplicationLayer.Services.AuthServices.RegisterService;

/// Handles register request
public interface IRegisterService
{
    /// Handle <see cref="RegisterApplicantRequest"/>
    Task RegisterApplicant(RegisterApplicantRequest request, CancellationToken cancellationToken);

    /// Handles <see cref="RegisterRequest"/> and adds approving authority account
    Task RegisterAuthority(RegisterRequest request, CancellationToken cancellationToken);
}