using ApplicationLayer.Services.AuthServices.Common;
using ApplicationLayer.Services.AuthServices.NeededServices;
using FluentValidation;

namespace ApplicationLayer.Services.AuthServices.Requests.Validation;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator(IUsersRepository users, IValidator<AuthData> authDataValidator)
    {
        RuleFor(r => r.AuthData)
            .NotEmpty()
            .SetValidator(authDataValidator)
            .MustAsync(async (authData, ct) => await users.FindByEmailAsync(authData.Email, ct) is null)
            .WithMessage("Email already exists");
    }
}
