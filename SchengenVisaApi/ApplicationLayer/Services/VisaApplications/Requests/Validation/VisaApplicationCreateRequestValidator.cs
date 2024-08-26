using ApplicationLayer.InfrastructureServicesInterfaces;
using ApplicationLayer.Services.Applicants.NeededServices;
using Domains;
using Domains.VisaApplicationDomain;
using FluentValidation;

namespace ApplicationLayer.Services.VisaApplications.Requests.Validation;

public class VisaApplicationCreateRequestValidator : AbstractValidator<VisaApplicationCreateRequest>
{
    public VisaApplicationCreateRequestValidator(
        IValidator<ReentryPermit?> reentryPermitValidator,
        IValidator<PastVisa> pastVisaValidator,
        IValidator<PermissionToDestCountry?> permissionToDestCountryValidator,
        IValidator<PastVisit> pastVisitValidator,
        IApplicantsRepository applicants,
        IUserIdProvider userIdProvider)
    {
        RuleFor(r => r.ReentryPermit)
            .NotEmpty()
            .WithMessage("Non-residents must provide re-entry permission")
            .SetValidator(reentryPermitValidator)
            .WhenAsync(async (_, ct) =>
                await applicants.IsApplicantNonResidentByUserId(userIdProvider.GetUserId(), ct));

        RuleFor(r => r.DestinationCountry)
            .NotEmpty()
            .WithMessage("Destination country can not be empty");

        RuleFor(r => r.VisaCategory)
            .IsInEnum();

        RuleFor(r => r.RequestedNumberOfEntries)
            .IsInEnum();

        RuleFor(r => r.ValidDaysRequested)
            .GreaterThan(0)
            .WithMessage($"Valid days requested should be positive number and less than {ConfigurationConstraints.MaxValidDays}")
            .LessThanOrEqualTo(ConfigurationConstraints.MaxValidDays)
            .WithMessage($"Valid days requested must be less than or equal to {ConfigurationConstraints.MaxValidDays}");

        RuleForEach(r => r.PastVisas)
            .SetValidator(pastVisaValidator);

        When(r => r.VisaCategory == VisaCategory.Transit,
            () =>
                RuleFor(r => r.PermissionToDestCountry)
                    .SetValidator(permissionToDestCountryValidator));

        RuleForEach(r => r.PastVisits)
            .SetValidator(pastVisitValidator);
    }
}
