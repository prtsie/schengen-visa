using Domains;
using FluentValidation;

namespace ApplicationLayer.Services.Users.Requests.Validation
{
    public class ChangeUserAuthDataRequestValidator : AbstractValidator<ChangeUserAuthDataRequest>
    {
        public ChangeUserAuthDataRequestValidator()
        {
            RuleFor(r => r.NewAuthData)
                .NotEmpty();

            RuleFor(r => r.NewAuthData.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Email should be valid")
                .MaximumLength(ConfigurationConstraints.EmailLength)
                .WithMessage($"Email address length must be less than {ConfigurationConstraints.EmailLength}");
        }
    }
}
