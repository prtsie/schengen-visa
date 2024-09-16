using BlazorWebAssemblyVisaApiClient.Validation.Applicants.Models;
using FluentValidation;
using VisaApiClient;

namespace BlazorWebAssemblyVisaApiClient.Validation.Auth;

public class RegisterRequestModelValidator : AbstractValidator<RegisterRequestModel>
{
    public RegisterRequestModelValidator(IValidator<AuthData> authDataValidator)
    {
        RuleFor(r => r.AuthData)
            .NotEmpty()
            .SetValidator(authDataValidator);
    }
}
