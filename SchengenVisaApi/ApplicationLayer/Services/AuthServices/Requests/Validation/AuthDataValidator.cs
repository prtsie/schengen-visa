using ApplicationLayer.Services.AuthServices.Common;
using ApplicationLayer.Services.AuthServices.NeededServices;
using Domains;
using FluentValidation;

namespace ApplicationLayer.Services.AuthServices.Requests.Validation
{
    public class AuthDataValidator : AbstractValidator<AuthData>
    {
        public AuthDataValidator(IUsersRepository users)
        {
            RuleFor(d => d.Email)
                .NotEmpty()
                .WithMessage("Email can not be empty")
                .EmailAddress()
                .WithMessage("Email must be valid")
                .MaximumLength(ConfigurationConstraints.EmailLength)
                .WithMessage($"Email length must be less than {ConfigurationConstraints.EmailLength}")
                .MustAsync(async (email, ct) =>
                {
                    return await users.FindByEmailAsync(email, ct) is null;
                })
                .WithMessage("Email already exists");

            RuleFor(d => d.Password)
                .NotEmpty()
                .WithMessage("Password can not be empty")
                .MaximumLength(ConfigurationConstraints.PasswordLength)
                .WithMessage($"Password length must be less than {ConfigurationConstraints.PasswordLength}");
        }
    }
}
