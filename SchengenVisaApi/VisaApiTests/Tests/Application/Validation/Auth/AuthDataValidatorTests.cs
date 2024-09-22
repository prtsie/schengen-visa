using System.Text;
using ApplicationLayer.Services.AuthServices.Common;
using ApplicationLayer.Services.AuthServices.Requests.Validation;
using Domains;
using FluentAssertions;
using FluentValidation;
using VisaApi.Fakers.Auth;
using Xunit;

namespace VisaApi.Tests.Application.Validation.Auth;

public class AuthDataValidatorTests
{
    private readonly static IValidator<AuthData> validator = new AuthDataValidator();
    private readonly static AuthDataFaker faker = new();

    /// <summary>
    /// Test for <see cref="AuthData"/> validator that should return validation error for invalid email
    /// </summary>
    [Fact]
    private async Task ValidateForInvalidEmailShouldReturnError()
    {
            var authData = faker.Generate();
            authData.Email = "alsdas'dsa";

            var result = await validator.ValidateAsync(authData);

            result.Errors.Should()
                .HaveCount(1)
                .And.Contain(error => error.PropertyName == nameof(authData.Email));
        }

    /// <summary>
    /// Test for <see cref="AuthData"/> validator that should return validation error for too long email
    /// </summary>
    [Fact]
    private async Task ValidateForLongEmailShouldReturnError()
    {
            var authData = faker.Generate();
            var stringBuilder = new StringBuilder();
            stringBuilder.Append('d', ConfigurationConstraints.EmailLength);
            stringBuilder.Append("@mail.ru");
            authData.Email = stringBuilder.ToString();

            var result = await validator.ValidateAsync(authData);

            result.Errors.Should()
                .HaveCount(1)
                .And.Contain(error => error.PropertyName == nameof(authData.Email));
        }

    /// <summary>
    /// Test for <see cref="AuthData"/> validator that should return no errors for valid email
    /// </summary>
    [Fact]
    private async Task ValidateForValidEmailShouldReturnNoError()
    {
            var authData = faker.Generate();

            var result = await validator.ValidateAsync(authData);

            result.Errors.Where(error => error.PropertyName == nameof(authData.Email))
                .Should().BeEmpty();
        }

    /// <summary>
    /// Test for <see cref="AuthData"/> validator that should return validation error for empty password
    /// </summary>
    [Fact]
    private async Task ValidateForEmptyPasswordShouldReturnError()
    {
            var authData = faker.Generate();
            authData.Password = string.Empty;

            var result = await validator.ValidateAsync(authData);

            result.Errors.Should()
                .HaveCount(1)
                .And.Contain(error => error.PropertyName == nameof(authData.Password));
        }

    /// <summary>
    /// Test for <see cref="AuthData"/> validator that should return validation error for too long password
    /// </summary>
    [Fact]
    private async Task ValidateForLongPasswordShouldReturnError()
    {
            var authData = faker.Generate();
            var stringBuilder = new StringBuilder();
            stringBuilder.Append('d', ConfigurationConstraints.PasswordLength + 1);
            authData.Password = stringBuilder.ToString();

            var result = await validator.ValidateAsync(authData);

            result.Errors.Should()
                .HaveCount(1)
                .And.Contain(error => error.PropertyName == nameof(authData.Password));
        }

    /// <summary>
    /// Test for <see cref="AuthData"/> validator that should return no errors for valid password
    /// </summary>
    [Fact]
    private async Task ValidateForValidPasswordShouldReturnNoError()
    {
            var authData = faker.Generate();

            var result = await validator.ValidateAsync(authData);

            result.Errors.Where(error => error.PropertyName == nameof(authData.Password))
                .Should().BeEmpty();
        }
}