using ApplicationLayer.GeneralExceptions;

namespace ApplicationLayer.Services.GeneralExceptions
{
    /// Exception to throw when can't complete some action on entity(delete or something) because it's needed for other entities
    public class EntityUsedInDatabaseException(string message) : ApiException(message);
}
