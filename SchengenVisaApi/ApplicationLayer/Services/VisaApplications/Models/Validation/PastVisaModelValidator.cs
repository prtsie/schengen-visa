using ApplicationLayer.InfrastructureServicesInterfaces;
using Domains;
using FluentValidation;

namespace ApplicationLayer.Services.VisaApplications.Models.Validation;

public class PastVisaModelValidator : AbstractValidator<PastVisaModel>
{
    public PastVisaModelValidator(IDateTimeProvider dateTimeProvider)
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
            .WithMessage("Name of past visa can not be empty")
            .Matches(Constants.EnglishPhraseRegex)
            .WithMessage("Name of past visa can contain only english letters, digits and special symbols")
            .MaximumLength(ConfigurationConstraints.VisaNameLength)
            .WithMessage($"Past visa name length must be less than {ConfigurationConstraints.VisaNameLength}");
    }
}
