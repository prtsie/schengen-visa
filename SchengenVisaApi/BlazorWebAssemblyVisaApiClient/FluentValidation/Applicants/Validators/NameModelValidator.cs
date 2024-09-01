﻿using FluentValidation;
using VisaApiClient;

namespace BlazorWebAssemblyVisaApiClient.FluentValidation.Applicants.Validators;

public class NameModelValidator : AbstractValidator<NameModel>
{
    public NameModelValidator()
    {
        RuleFor(m => m.FirstName)
            .NotEmpty()
            .WithMessage("First Name can not be empty")
            .MaximumLength(ConfigurationConstraints.NameLength)
            .WithMessage($"First Name length must be less than {ConfigurationConstraints.NameLength}");

        RuleFor(m => m.Surname)
            .NotEmpty()
            .WithMessage("Surname can not be empty")
            .MaximumLength(ConfigurationConstraints.NameLength)
            .WithMessage($"Surname length must be less than {ConfigurationConstraints.NameLength}");

        RuleFor(m => m.Patronymic)
            .MaximumLength(ConfigurationConstraints.NameLength)
            .WithMessage($"Patronymic length must be less than {ConfigurationConstraints.NameLength}");
    }
}
