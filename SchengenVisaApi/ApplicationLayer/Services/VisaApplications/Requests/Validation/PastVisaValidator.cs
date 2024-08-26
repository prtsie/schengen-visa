using ApplicationLayer.InfrastructureServicesInterfaces;
using Domains.VisaApplicationDomain;
using FluentValidation;

namespace ApplicationLayer.Services.VisaApplications.Requests.Validation;

public class PastVisaValidator : AbstractValidator<PastVisa>
{
    public PastVisaValidator(IDateTimeProvider dateTimeProvider)
    {
            RuleFor(v => v.ExpirationDate)
                .NotEmpty()
                .WithMessage("Expiration date of past visa can not be empty")
                .GreaterThan(v => v.IssueDate)
                .WithMessage("Past visa expiration date can not be earlier than issue date");

            RuleFor(v => v.IssueDate)
                .NotEmpty()
                .WithMessage("Issue date of past visa can not be empty")
                .LessThan(dateTimeProvider.Now())
                .WithMessage("Issue date of past visa must be in past");

            RuleFor(v => v.Name)
                .NotEmpty()
                .WithMessage("Name of past visa can not be empty");
        }
}