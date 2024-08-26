using ApplicationLayer.InfrastructureServicesInterfaces;
using Domains;
using Domains.ApplicantDomain;
using FluentValidation;

namespace ApplicationLayer.Services.AuthServices.Requests.Validation;

public class PassportValidator : AbstractValidator<Passport>
{
    public PassportValidator(IDateTimeProvider dateTimeProvider)
    {
            RuleFor(r => r.Issuer)
                .NotEmpty()
                .WithMessage("Passport issuer can not be empty")
                .MaximumLength(ConfigurationConstraints.IssuerNameLength)
                .WithMessage($"Passport issuer length must be less than {ConfigurationConstraints.IssuerNameLength}");

            RuleFor(r => r.Number)
                .NotEmpty()
                .WithMessage("Passport number can not be empty")
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