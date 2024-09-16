namespace ApplicationLayer.GeneralExceptions;

/// Exception to throw when entity not found
public class EntityNotFoundException(string message) : ApiException(message);
