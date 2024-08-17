using ApplicationLayer.DataAccessingServices.AuthServices.Requests;

namespace ApplicationLayer.DataAccessingServices.AuthServices.RegisterService
{
    /// Handles <see cref="RegisterApplicantRequest"/>
    public interface IRegisterService
    {
        /// Handle <see cref="RegisterApplicantRequest"/>
        Task Register(RegisterApplicantRequest request, CancellationToken cancellationToken);
    }
}
