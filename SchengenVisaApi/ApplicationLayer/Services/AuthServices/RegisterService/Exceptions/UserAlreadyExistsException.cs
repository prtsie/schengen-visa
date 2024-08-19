using ApplicationLayer.GeneralExceptions;
using ApplicationLayer.Services.AuthServices.Requests;

namespace ApplicationLayer.Services.AuthServices.RegisterService.Exceptions
{
    public class UserAlreadyExistsException(RegisterApplicantRequest request) : AlreadyExistsException($"User with email '{request.Email}' already exists");
}
