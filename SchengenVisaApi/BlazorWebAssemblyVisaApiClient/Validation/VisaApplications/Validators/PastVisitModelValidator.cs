using BlazorWebAssemblyVisaApiClient.Infrastructure.Services.DateTimeProvider;
using FluentValidation;
using VisaApiClient;

namespace BlazorWebAssemblyVisaApiClient.Validation.VisaApplications.Validators;

public class PastVisitModelValidator : AbstractValidator<PastVisitModel>
{
    public PastVisitModelValidator(IDateTimeProvider dateTimeProvider)
    {
        RuleFor(v => v.StartDate)
            .NotEmpty()
            .WithMessage("Start date of past visit can not be empty")
            .LessThan(v => v.EndDate)
            .WithMessage("Start date of past visit must be earlier than end date")
            .LessThan(dateTimeProvider.Now())
            .WithMessage("Start date of past visit must be in past");

        RuleFor(v => v.EndDate)
            .NotEmpty()
            .WithMessage("End date of past visit can not be empty");

        RuleFor(v => v.DestinationCountry)
            .NotEmpty()
            .WithMessage("Destination Country of past visit can not be null")
            .MaximumLength(ConfigurationConstraints.CountryNameLength)
            .WithMessage($"Destination Country of past visit length must be less than {ConfigurationConstraints.CountryNameLength}");
    }
}
