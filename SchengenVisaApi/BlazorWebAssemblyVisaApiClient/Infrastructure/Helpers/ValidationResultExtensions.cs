using System.Text;
using FluentValidation.Results;

namespace BlazorWebAssemblyVisaApiClient.Infrastructure.Helpers;

public static class ValidationResultExtensions
{
    public static string ToErrorsString(this ValidationResult validationResult)
        => ErrorsToString(validationResult.Errors.Select(e => e.ErrorMessage));

    private static string ErrorsToString(IEnumerable<string> errors)
    {
            var stringBuilder = new StringBuilder();
            foreach (var error in errors)
            {
                stringBuilder.Append($"{error}<br/>");
            }

            return stringBuilder.ToString();
        }
}