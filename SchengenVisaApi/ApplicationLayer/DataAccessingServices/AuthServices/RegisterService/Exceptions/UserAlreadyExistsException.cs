using ApplicationLayer.DataAccessingServices.AuthServices.Requests;
using ApplicationLayer.GeneralExceptions;

namespace ApplicationLayer.DataAccessingServices.AuthServices.RegisterService.Exceptions
{
    public class UserAlreadyExistsException(RegisterApplicantRequest request) : AlreadyExistsException($"User with email '{request.Email}' already exists");
}
