namespace ApplicationLayer.GeneralExceptions
{
    public class AlreadyExistsException(string message) : ApiException(message);
}
