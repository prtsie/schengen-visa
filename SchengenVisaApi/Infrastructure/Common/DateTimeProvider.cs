using ApplicationLayer.GeneralNeededServices;

namespace Infrastructure.Common
{
    /// Implements <see cref="IDateTimeProvider"/>
    public class DateTimeProvider : IDateTimeProvider
    {
        DateTime IDateTimeProvider.Now() => DateTime.Now;
    }
}
