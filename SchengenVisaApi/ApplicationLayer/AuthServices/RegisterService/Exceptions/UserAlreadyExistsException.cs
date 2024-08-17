using ApplicationLayer.AuthServices.Requests;

namespace ApplicationLayer.AuthServices.RegisterService.Exceptions
{
    public class UserAlreadyExistsException(RegisterApplicantRequest request) : Exception($"User with email '{request.Email}' already exists");
}
