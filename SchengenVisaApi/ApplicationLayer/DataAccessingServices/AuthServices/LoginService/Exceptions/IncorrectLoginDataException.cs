using ApplicationLayer.GeneralExceptions;

namespace ApplicationLayer.DataAccessingServices.AuthServices.LoginService.Exceptions
{
    public class IncorrectLoginDataException() : ApiException("Incorrect email or password");
}
