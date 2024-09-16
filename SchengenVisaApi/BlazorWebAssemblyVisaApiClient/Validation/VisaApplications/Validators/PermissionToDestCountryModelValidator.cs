using BlazorWebAssemblyVisaApiClient.Infrastructure.Services.DateTimeProvider;
using FluentValidation;
using VisaApiClient;

namespace BlazorWebAssemblyVisaApiClient.Validation.VisaApplications.Validators;

public class PermissionToDestCountryModelValidator : AbstractValidator<PermissionToDestCountryModel?>
{
    public PermissionToDestCountryModelValidator(IDateTimeProvider dateTimeProvider)
    {
        RuleFor(p => p!.ExpirationDate)
            .NotEmpty()
            .WithMessage("Expiration date of permission to destination Country can not be empty")
            .GreaterThan(dateTimeProvider.Now())
            .WithMessage("Permission to destination Country must not be expired");

        RuleFor(p => p!.Issuer)
            .NotEmpty()
            .WithMessage("Issuer of permission for destination Country can not be empty")
            .Matches(Constants.EnglishPhraseRegex)
            .WithMessage("Issuer of permission for destination Country can contain only english letters, digits and special symbols")
            .MaximumLength(ConfigurationConstraints.IssuerNameLength)
            .WithMessage($"Issuer of permission to destination Country length must be less than {ConfigurationConstraints.IssuerNameLength}");
    }
}
