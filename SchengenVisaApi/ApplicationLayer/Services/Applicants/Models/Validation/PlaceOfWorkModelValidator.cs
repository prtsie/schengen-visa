using Domains;
using FluentValidation;

namespace ApplicationLayer.Services.Applicants.Models.Validation;

public class PlaceOfWorkModelValidator : AbstractValidator<PlaceOfWorkModel>
{
    public PlaceOfWorkModelValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage("Place of work name can not be empty")
            .Matches(Constants.EnglishPhraseRegex)
            .WithMessage("Place of work name field can contain only english letters, digits and special symbols")
            .MaximumLength(ConfigurationConstraints.PlaceOfWorkNameLength)
            .WithMessage($"Place of work name length must be less than {ConfigurationConstraints.PlaceOfWorkNameLength}");

        RuleFor(p => p.PhoneNum)
            .NotEmpty()
            .WithMessage("Place of work phone number can not be empty")
            .Matches(Constants.PhoneNumRegex)
            .WithMessage("Place of work phone number field must be valid")
            .MaximumLength(ConfigurationConstraints.PhoneNumberLength)
            .WithMessage(
                $"Phone number length must be in range from {ConfigurationConstraints.PhoneNumberMinLength} to {ConfigurationConstraints.PhoneNumberLength}")
            .MinimumLength(ConfigurationConstraints.PhoneNumberMinLength)
            .WithMessage(
                $"Phone number length must be in range from {ConfigurationConstraints.PhoneNumberMinLength} to {ConfigurationConstraints.PhoneNumberLength}");

        RuleFor(p => p.Address)
            .NotEmpty()
            .WithMessage("Place of work address can not be empty");

        RuleFor(p => p.Address.Country)
            .NotEmpty()
            .WithMessage("Country name of place of work can not be empty")
            .Matches(Constants.EnglishPhraseRegex)
            .WithMessage("Place of work Country field can contain only english letters, digits and special symbols")
            .MaximumLength(ConfigurationConstraints.CountryNameLength)
            .WithMessage($"Country name of place of work length must be less than {ConfigurationConstraints.CountryNameLength}");

        RuleFor(p => p.Address.City)
            .NotEmpty()
            .WithMessage("City name of place of work can not be empty")
            .Matches(Constants.EnglishPhraseRegex)
            .WithMessage("Place of work City field can contain only english letters, digits and special symbols")
            .MaximumLength(ConfigurationConstraints.CityNameLength)
            .WithMessage($"City name of place of work length must be less than {ConfigurationConstraints.CityNameLength}");

        RuleFor(p => p.Address.Street)
            .NotEmpty()
            .WithMessage("Street name of place of work can not be empty")
            .Matches(Constants.EnglishPhraseRegex)
            .WithMessage("Place of work Street field can contain only english letters, digits and special symbols")
            .MaximumLength(ConfigurationConstraints.StreetNameLength)
            .WithMessage($"Street name of place of work length must be less than {ConfigurationConstraints.StreetNameLength}");

        RuleFor(p => p.Address.Building)
            .NotEmpty()
            .WithMessage("Building of place of work can not be empty")
            .Matches(Constants.EnglishPhraseRegex)
            .WithMessage("Place of work building field can contain only english letters, digits and special symbols")
            .MaximumLength(ConfigurationConstraints.CountryNameLength)
            .WithMessage($"Building of place of work length must be less than {ConfigurationConstraints.BuildingNumberLength}");
    }
}
