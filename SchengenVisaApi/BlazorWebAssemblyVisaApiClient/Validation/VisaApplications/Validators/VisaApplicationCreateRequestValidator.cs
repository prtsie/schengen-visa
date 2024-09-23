using BlazorWebAssemblyVisaApiClient.Infrastructure.Services.UserDataProvider;
using BlazorWebAssemblyVisaApiClient.Validation.VisaApplications.Models;
using FluentValidation;
using VisaApiClient;

namespace BlazorWebAssemblyVisaApiClient.Validation.VisaApplications.Validators;

public class VisaApplicationCreateRequestValidator : AbstractValidator<VisaApplicationCreateRequestModel>
{
    public VisaApplicationCreateRequestValidator(
        IValidator<ReentryPermitModel?> reentryPermitModelValidator,
        IValidator<PastVisaModel> pastVisaModelValidator,
        IValidator<PermissionToDestCountryModel?> permissionToDestCountryModelValidator,
        IValidator<PastVisitModel> pastVisitModelValidator,
        IUserDataProvider userDataProvider)
    {
        RuleFor(r => r.PermissionToDestCountry)
            .NotEmpty()
            .WithMessage("For transit you must provide permission to destination country")
            .SetValidator(permissionToDestCountryModelValidator)
            .When(r => r.VisaCategory is VisaCategoryModel.Transit);

        RuleFor(r => r.ReentryPermit)
            .NotEmpty()
            .WithMessage("Non-residents must provide re-entry permission")
            .SetValidator(reentryPermitModelValidator)
            .WhenAsync(async (_, _) =>
                (await userDataProvider.GetApplicant()).IsNonResident);

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
            .SetValidator(pastVisaModelValidator);

        RuleForEach(r => r.PastVisits)
            .SetValidator(pastVisitModelValidator);
    }
}
