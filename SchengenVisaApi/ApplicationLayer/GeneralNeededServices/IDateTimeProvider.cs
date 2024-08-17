namespace ApplicationLayer.GeneralNeededServices
{
    public interface IDateTimeProvider
    {
        /// Returns current date and time
        DateTime Now();
    }
}
