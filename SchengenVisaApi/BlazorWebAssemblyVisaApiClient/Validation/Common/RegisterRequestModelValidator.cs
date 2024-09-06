using BlazorWebAssemblyVisaApiClient.Validation.Applicants.Models;
using FluentValidation;
using VisaApiClient;

namespace BlazorWebAssemblyVisaApiClient.Validation.Common;

public class RegisterRequestModelValidator : AbstractValidator<RegisterRequestModel>
{
    public RegisterRequestModelValidator(IValidator<AuthData> authDataValidator)
    {
        RuleFor(r => r.AuthData)
            .SetValidator(authDataValidator);
    }
}
