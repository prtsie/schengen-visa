using BlazorWebAssemblyVisaApiClient.Infrastructure.Services.DateTimeProvider;
using BlazorWebAssemblyVisaApiClient.Validation.Applicants.Models;
using FluentValidation;
using VisaApiClient;
using PlaceOfWorkModel = BlazorWebAssemblyVisaApiClient.Validation.Applicants.Models.PlaceOfWorkModel;

namespace BlazorWebAssemblyVisaApiClient.Validation.Applicants.Validators;

public class RegisterApplicantRequestValidator : AbstractValidator<RegisterApplicantRequestModel>
{
    public RegisterApplicantRequestValidator(
        IDateTimeProvider dateTimeProvider,
        IValidator<NameModel> nameValidator,
        IValidator<RegisterRequestModel> registerRequestValidator,
        IValidator<PassportModel> passportValidator,
        IValidator<PlaceOfWorkModel> placeOfWorkModelValidator)
    {
        RuleFor(r => r.RegisterRequest)
            .NotEmpty()
            .SetValidator(registerRequestValidator);

        RuleFor(r => r.ApplicantName)
            .NotEmpty()
            .SetValidator(nameValidator);

        RuleFor(r => r.FatherName)
            .NotEmpty()
            .SetValidator(nameValidator);

        RuleFor(r => r.MotherName)
            .NotEmpty()
            .SetValidator(nameValidator);

        RuleFor(r => r.Passport)
            .NotEmpty()
            .SetValidator(passportValidator);

        RuleFor(r => r.BirthDate)
            .NotEmpty()
            .WithMessage("Birth date can not be empty")
            .LessThanOrEqualTo(dateTimeProvider.Now().AddYears(-ConfigurationConstraints.ApplicantMinAge))
            .WithMessage($"Applicant must be older than {ConfigurationConstraints.ApplicantMinAge}");

        RuleFor(r => r.CountryOfBirth)
            .NotEmpty()
            .WithMessage("Country of birth can not be empty")
            .Matches(Constants.EnglishPhraseRegex)
            .WithMessage("Country of birth field can contain only english letters, digits and special symbols")
            .MaximumLength(ConfigurationConstraints.CountryNameLength)
            .WithMessage($"Country of birth name length must be less than {ConfigurationConstraints.CountryNameLength}");

        RuleFor(r => r.CityOfBirth)
            .NotEmpty()
            .WithMessage("City of birth can not be empty")
            .Matches(Constants.EnglishPhraseRegex)
            .WithMessage("City of birth field can contain only english letters, digits and special symbols")
            .MaximumLength(ConfigurationConstraints.CityNameLength)
            .WithMessage($"City of birth name length must be less than {ConfigurationConstraints.CityNameLength}");

        RuleFor(r => r.Citizenship)
            .NotEmpty()
            .WithMessage("Citizenship can not be empty")
            .Matches(Constants.EnglishPhraseRegex)
            .WithMessage("Citizenship field can contain only english letters, digits and special symbols")
            .MaximumLength(ConfigurationConstraints.CitizenshipLength)
            .WithMessage($"Citizenship length must be less than {ConfigurationConstraints.CitizenshipLength}");

        RuleFor(r => r.CitizenshipByBirth)
            .NotEmpty()
            .WithMessage("Citizenship by birth can not be empty")
            .Matches(Constants.EnglishPhraseRegex)
            .WithMessage("Citizenship by birth field can contain only english letters, digits and special symbols")
            .MaximumLength(ConfigurationConstraints.CitizenshipLength)
            .WithMessage($"Citizenship by birth length must be less than {ConfigurationConstraints.CitizenshipLength}");

        RuleFor(r => r.Gender)
            .IsInEnum();

        RuleFor(r => r.MaritalStatus)
            .IsInEnum();

        RuleFor(r => r.JobTitle)
            .NotEmpty()
            .WithMessage("Title of job can not be empty")
            .Matches(Constants.EnglishPhraseRegex)
            .WithMessage("Title of job field can contain only english letters, digits and special symbols")
            .MaximumLength(ConfigurationConstraints.JobTitleLength)
            .WithMessage($"Title of job length must be less than {ConfigurationConstraints.JobTitleLength}");

        RuleFor(r => r.PlaceOfWork)
            .NotEmpty()
            .SetValidator(placeOfWorkModelValidator);
    }
}
