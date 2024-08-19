using ApplicationLayer.GeneralExceptions;

namespace ApplicationLayer.Services.AuthServices.LoginService.Exceptions
{
    public class IncorrectLoginDataException() : ApiException("Incorrect email or password");
}
