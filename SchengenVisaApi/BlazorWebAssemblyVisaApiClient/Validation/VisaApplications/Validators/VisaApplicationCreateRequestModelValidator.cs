using BlazorWebAssemblyVisaApiClient.Validation.VisaApplications.Models;
using FluentValidation;
using VisaApiClient;

namespace BlazorWebAssemblyVisaApiClient.Validation.VisaApplications.Validators;

public class VisaApplicationCreateRequestModelValidator : AbstractValidator<VisaApplicationCreateRequestModel>
{
    public VisaApplicationCreateRequestModelValidator(
        IValidator<PastVisaModel> pastVisaModelValidator,
        IValidator<PermissionToDestCountryModel?> permissionToDestCountryModelValidator,
        IValidator<PastVisitModel> pastVisitModelValidator)
    {
        RuleFor(r => r.PermissionToDestCountry)
            .NotEmpty()
            .WithMessage("For transit you must provide permission to destination country")
            .SetValidator(permissionToDestCountryModelValidator)
            .When(r => r.VisaCategory is VisaCategory.Transit);

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
