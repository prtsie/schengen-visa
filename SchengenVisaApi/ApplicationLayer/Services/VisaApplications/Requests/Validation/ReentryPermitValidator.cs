using ApplicationLayer.InfrastructureServicesInterfaces;
using Domains;
using Domains.VisaApplicationDomain;
using FluentValidation;

namespace ApplicationLayer.Services.VisaApplications.Requests.Validation;

public class ReentryPermitValidator : AbstractValidator<ReentryPermit?>
{
    public ReentryPermitValidator(IDateTimeProvider dateTimeProvider)
    {
            RuleFor(p => p!.Number)
                .NotEmpty()
                .WithMessage("Re-entry permit number can not be empty")
                .MaximumLength(ConfigurationConstraints.ReentryPermitNumberLength)
                .WithMessage($"Re-entry permit number length must be less than {ConfigurationConstraints.ReentryPermitNumberLength}");

            RuleFor(p => p!.ExpirationDate)
                .NotEmpty()
                .WithMessage("Re-entry permit expiration date can not be empty")
                .GreaterThan(dateTimeProvider.Now())
                .WithMessage("Re-entry permit must not be expired");
        }
}