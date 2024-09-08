using BlazorWebAssemblyVisaApiClient.Infrastructure.Services.DateTimeProvider;
using FluentValidation;
using VisaApiClient;

namespace BlazorWebAssemblyVisaApiClient.Validation.VisaApplications.Validators;

public class ReentryPermitModelValidator : AbstractValidator<ReentryPermitModel?>
{
    public ReentryPermitModelValidator(IDateTimeProvider dateTimeProvider)
    {
        RuleFor(p => p!.Number)
            .NotEmpty()
            .WithMessage("Re-entry permit number can not be empty")
            .Matches(Constants.EnglishPhraseRegex)
            .WithMessage("Re-entry permit number can contain only english letters, digits and special symbols")
            .MaximumLength(ConfigurationConstraints.ReentryPermitNumberLength)
            .WithMessage($"Re-entry permit number length must be less than {ConfigurationConstraints.ReentryPermitNumberLength}");

        RuleFor(p => p!.ExpirationDate)
            .NotEmpty()
            .WithMessage("Re-entry permit expiration date can not be empty")
            .GreaterThan(dateTimeProvider.Now())
            .WithMessage("Re-entry permit must not be expired");
    }
}
