using Domains;
using Domains.ApplicantDomain;
using FluentValidation;

namespace ApplicationLayer.Services.AuthServices.Requests.Validation
{
    public class NameValidator : AbstractValidator<Name>
    {
        public NameValidator()
        {
            RuleFor(m => m.FirstName)
                .NotEmpty()
                .WithMessage("First Name can not be empty")
                .MaximumLength(ConfigurationConstraints.NameLength)
                .WithMessage($"First Name length must be less than {ConfigurationConstraints.NameLength}");

            RuleFor(m => m.Surname)
                .NotEmpty()
                .WithMessage("Surname can not be empty")
                .MaximumLength(ConfigurationConstraints.NameLength)
                .WithMessage($"Surname length must be less than {ConfigurationConstraints.NameLength}");

            RuleFor(m => m.Patronymic)
                .MaximumLength(ConfigurationConstraints.NameLength)
                .WithMessage($"Patronymic length must be less than {ConfigurationConstraints.NameLength}");
        }
    }
}
