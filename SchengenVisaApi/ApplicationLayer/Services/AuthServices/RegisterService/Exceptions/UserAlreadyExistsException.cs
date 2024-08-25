using ApplicationLayer.GeneralExceptions;
using ApplicationLayer.Services.AuthServices.Requests;

namespace ApplicationLayer.Services.AuthServices.RegisterService.Exceptions
{
    public class UserAlreadyExistsException(RegisterRequest request) : AlreadyExistsException($"User with email '{request.AuthData.Email}' already exists");
}
