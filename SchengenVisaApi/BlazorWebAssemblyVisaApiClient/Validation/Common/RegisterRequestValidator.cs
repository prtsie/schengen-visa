using BlazorWebAssemblyVisaApiClient.Validation.Applicants.Models;
using FluentValidation;
using VisaApiClient;

namespace BlazorWebAssemblyVisaApiClient.Validation.Common;

public class RegisterRequestValidator : AbstractValidator<RegisterRequestModel>
{
    public RegisterRequestValidator(IValidator<AuthData> authDataValidator)
    {
        RuleFor(r => r.AuthData)
            .NotEmpty()
            .SetValidator(authDataValidator);
    }
}
