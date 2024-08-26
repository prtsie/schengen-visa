using ApplicationLayer.Services.Applicants.Models;
using Domains;
using FluentValidation;

namespace ApplicationLayer.Services.AuthServices.Requests.Validation;

public class PlaceOfWorkModelValidator : AbstractValidator<PlaceOfWorkModel>
{
    public PlaceOfWorkModelValidator()
    {
            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage("Place of work name can not be empty")
                .MaximumLength(ConfigurationConstraints.PlaceOfWorkNameLength)
                .WithMessage($"Place of work name length must be less than {ConfigurationConstraints.PlaceOfWorkNameLength}");

            RuleFor(p => p.PhoneNum)
                .NotEmpty()
                .WithMessage("Place of work phone number can not be empty")
                .MaximumLength(ConfigurationConstraints.PhoneNumberLength)
                .WithMessage($"Phone number length must be in range from {ConfigurationConstraints.PhoneNumberMinLength} to {ConfigurationConstraints.PhoneNumberLength}")
                .MinimumLength(ConfigurationConstraints.PhoneNumberMinLength)
                .WithMessage($"Phone number length must be in range from {ConfigurationConstraints.PhoneNumberMinLength} to {ConfigurationConstraints.PhoneNumberLength}");

            RuleFor(p => p.Address.Country)
                .NotEmpty()
                .WithMessage("Country name of place of work can not be empty")
                .MaximumLength(ConfigurationConstraints.CountryNameLength)
                .WithMessage($"Country name of place of work length must be less than {ConfigurationConstraints.CountryNameLength}");

            RuleFor(p => p.Address.City)
                .NotEmpty()
                .WithMessage("City name of place of work can not be empty")
                .MaximumLength(ConfigurationConstraints.CityNameLength)
                .WithMessage($"City name of place of work length must be less than {ConfigurationConstraints.CityNameLength}");

            RuleFor(p => p.Address.Street)
                .NotEmpty()
                .WithMessage("Street name of place of work can not be empty")
                .MaximumLength(ConfigurationConstraints.StreetNameLength)
                .WithMessage($"Street name of place of work length must be less than {ConfigurationConstraints.StreetNameLength}");

            RuleFor(p => p.Address.Building)
                .NotEmpty()
                .WithMessage("Building of place of work can not be empty")
                .MaximumLength(ConfigurationConstraints.CountryNameLength)
                .WithMessage($"Building of place of work length must be less than {ConfigurationConstraints.BuildingNumberLength}");
        }
}