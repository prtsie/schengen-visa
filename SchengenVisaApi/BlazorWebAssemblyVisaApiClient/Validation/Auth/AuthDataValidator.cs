using FluentValidation;
using VisaApiClient;

namespace BlazorWebAssemblyVisaApiClient.Validation.Auth;

public class AuthDataValidator : AbstractValidator<AuthData>
{
    public AuthDataValidator()
    {
        RuleFor(d => d.Email)
            .NotEmpty()
            .WithMessage("Email can not be empty")
            .EmailAddress()
            .WithMessage("Email must be valid")
            .MaximumLength(ConfigurationConstraints.EmailLength)
            .WithMessage($"Email length must be less than {ConfigurationConstraints.EmailLength}");

        RuleFor(d => d.Password)
            .NotEmpty()
            .WithMessage("Password can not be empty")
            .Matches(Constants.EnglishPhraseRegex)
            .WithMessage("Password can contain only english letters, digits and special symbols")
            .MaximumLength(ConfigurationConstraints.PasswordLength)
            .WithMessage($"Password length must be less than {ConfigurationConstraints.PasswordLength}");
    }
}
