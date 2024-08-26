using ApplicationLayer.InfrastructureServicesInterfaces;
using Domains;
using Domains.VisaApplicationDomain;
using FluentValidation;

namespace ApplicationLayer.Services.VisaApplications.Requests.Validation
{
    public class PermissionToDestCountryValidator : AbstractValidator<PermissionToDestCountry?>
    {
        public PermissionToDestCountryValidator(IDateTimeProvider dateTimeProvider)
        {
            RuleFor(p => p!.ExpirationDate)
                .NotEmpty()
                .WithMessage("Expiration date of permission to destination Country can not be empty")
                .GreaterThan(dateTimeProvider.Now())
                .WithMessage("Permission to destination Country must not be expired");

            RuleFor(p => p!.Issuer)
                .NotEmpty()
                .WithMessage("Issuer of permission for destination Country can not be empty")
                .MaximumLength(ConfigurationConstraints.IssuerNameLength)
                .WithMessage($"Issuer of permission to destination Country length must be less than {ConfigurationConstraints.IssuerNameLength}");
        }
    }
}
