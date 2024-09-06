using System.ComponentModel.DataAnnotations;

namespace BlazorWebAssemblyVisaApiClient.Infrastructure.Helpers
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum value)
        {
            var enumMembers = value.GetType().GetMembers();
            var member = enumMembers.First(info => info.Name == value.ToString());
            var displayAttribute = (DisplayAttribute?)member
                .GetCustomAttributes(typeof(DisplayAttribute), false)
                .FirstOrDefault();
            var displayName = displayAttribute?.Name ?? value.ToString();
            return displayName;
        }
    }
}
