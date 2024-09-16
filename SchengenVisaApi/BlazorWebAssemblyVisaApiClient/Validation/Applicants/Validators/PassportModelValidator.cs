using BlazorWebAssemblyVisaApiClient.Infrastructure.Services.DateTimeProvider;
using FluentValidation;
using VisaApiClient;

namespace BlazorWebAssemblyVisaApiClient.Validation.Applicants.Validators;

public class PassportModelValidator : AbstractValidator<PassportModel>
{
    public PassportModelValidator(IDateTimeProvider dateTimeProvider)
    {
        RuleFor(r => r.Issuer)
            .NotEmpty()
            .WithMessage("Passport issuer can not be empty")
            .Matches(Constants.EnglishPhraseRegex)
            .WithMessage("Passport issuer field can contain only english letters, digits and special symbols")
            .MaximumLength(ConfigurationConstraints.IssuerNameLength)
            .WithMessage($"Passport issuer length must be less than {ConfigurationConstraints.IssuerNameLength}");

        RuleFor(r => r.Number)
            .NotEmpty()
            .WithMessage("Passport number can not be empty")
            .Matches(Constants.EnglishPhraseRegex)
            .WithMessage("Passport number field can contain only english letters, digits and special symbols")
            .MaximumLength(ConfigurationConstraints.PassportNumberLength)
            .WithMessage($"Passport number length must be less than {ConfigurationConstraints.PassportNumberLength}");

        RuleFor(r => r.ExpirationDate)
            .NotEmpty()
            .WithMessage("Passport expiration date can not be empty")
            .GreaterThan(dateTimeProvider.Now())
            .WithMessage("Can not approve visa for applicants with expired passport");

        RuleFor(r => r.IssueDate)
            .NotEmpty()
            .WithMessage("Passport issue date can not be empty")
            .LessThanOrEqualTo(dateTimeProvider.Now())
            .WithMessage("Passport issue date must be in past");
    }
}
