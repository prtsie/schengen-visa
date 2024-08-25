using ApplicationLayer.InfrastructureServicesInterfaces;
using Domains;
using Domains.VisaApplicationDomain;
using FluentValidation;

namespace ApplicationLayer.Services.VisaApplications.Requests.Validation
{
    public class PastVisitValidator : AbstractValidator<PastVisit>
    {
        public PastVisitValidator(IDateTimeProvider dateTimeProvider)
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
}
