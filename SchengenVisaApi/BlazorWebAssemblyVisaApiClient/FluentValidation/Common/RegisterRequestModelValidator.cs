using BlazorWebAssemblyVisaApiClient.FluentValidation.Applicants.Models;
using FluentValidation;
using VisaApiClient;

namespace BlazorWebAssemblyVisaApiClient.FluentValidation.Common;

public class RegisterRequestModelValidator : AbstractValidator<RegisterRequestModel>
{
    public RegisterRequestModelValidator(IValidator<AuthData> authDataValidator)
    {
        RuleFor(r => r.AuthData)
            .SetValidator(authDataValidator);
    }
}
