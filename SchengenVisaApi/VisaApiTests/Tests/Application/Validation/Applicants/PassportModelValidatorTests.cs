using System.Text;
using ApplicationLayer.InfrastructureServicesInterfaces;
using ApplicationLayer.Services.Applicants.Models;
using ApplicationLayer.Services.Applicants.Models.Validation;
using Domains;
using FluentAssertions;
using FluentValidation;
using VisaApi.Fakers.Applicants.Requests;
using VisaApi.Services;
using Xunit;

namespace VisaApi.Tests.Application.Validation.Applicants;

public class PassportModelValidatorTests
{
    private readonly static IDateTimeProvider dateTimeProvider = new TestDateTimeProvider();
    private readonly static IValidator<PassportModel> validator = new PassportModelValidator(dateTimeProvider);
    private readonly static PassportModelFaker faker = new(dateTimeProvider);

    /// <summary>
    /// Test for <see cref="PassportModel"/> validator that should return error for empty number
    /// </summary>
    [Fact]
    private async Task ValidateForEmptyNumberShouldReturnError()
    {
        var model = faker.Generate();
        model.Number = string.Empty;

        var result = await validator.ValidateAsync(model);

        result.Errors.Where(error => error.PropertyName == nameof(model.Number))
            .Should().HaveCount(1);
    }

    /// <summary>
    /// Test for <see cref="PassportModel"/> validator that should return error for too long number
    /// </summary>
    [Fact]
    private async Task ValidateForLongNumberShouldReturnError()
    {
        var model = faker.Generate();
        var stringBuilder = new StringBuilder();
        stringBuilder.Append('d', ConfigurationConstraints.PassportNumberLength + 1);
        model.Number = stringBuilder.ToString();

        var result = await validator.ValidateAsync(model);

        result.Errors.Where(error => error.PropertyName == nameof(model.Number))
            .Should().HaveCount(1);
    }

    /// <summary>
    /// Test for <see cref="PassportModel"/> validator that should return error for not valid number
    /// </summary>
    [Fact]
    private async Task ValidateForNotValidNumberShouldReturnError()
    {
        var model = faker.Generate();
        model.Number = "&?%$24asd\\]|";

        var result = await validator.ValidateAsync(model);

        result.Errors.Where(error => error.PropertyName == nameof(model.Number))
            .Should().HaveCount(1);
    }

    /// <summary>
    /// Test for <see cref="PassportModel"/> validator that should return error for empty issuer
    /// </summary>
    [Fact]
    private async Task ValidateForEmptyIssuerShouldReturnError()
    {
        var model = faker.Generate();
        model.Issuer = string.Empty;

        var result = await validator.ValidateAsync(model);

        result.Errors.Where(error => error.PropertyName == nameof(model.Issuer))
            .Should().HaveCount(1);
    }

    /// <summary>
    /// Test for <see cref="PassportModel"/> validator that should return error for too long issuer
    /// </summary>
    [Fact]
    private async Task ValidateForLongIssuerShouldReturnError()
    {
        var model = faker.Generate();
        var stringBuilder = new StringBuilder();
        stringBuilder.Append('d', ConfigurationConstraints.IssuerNameLength + 1);
        model.Issuer = stringBuilder.ToString();

        var result = await validator.ValidateAsync(model);

        result.Errors.Where(error => error.PropertyName == nameof(model.Issuer))
            .Should().HaveCount(1);
    }

    /// <summary>
    /// Test for <see cref="PassportModel"/> validator that should return error for not valid issuer
    /// </summary>
    [Fact]
    private async Task ValidateForNotValidIssuerShouldReturnError()
    {
        var model = faker.Generate();
        model.Issuer = "&?%$24asd\\]|";

        var result = await validator.ValidateAsync(model);

        result.Errors.Where(error => error.PropertyName == nameof(model.Issuer))
            .Should().HaveCount(1);
    }

    /// <summary>
    /// Test for <see cref="PassportModel"/> validator that should return error for expired passport
    /// </summary>
    [Fact]
    private async Task ValidateForExpiredPassportShouldReturnError()
    {
        var model = faker.Generate();
        model.ExpirationDate = dateTimeProvider.Now().AddDays(-10);
        model.IssueDate = model.ExpirationDate.AddDays(-10);

        var result = await validator.ValidateAsync(model);

        result.Errors.Where(error => error.PropertyName == nameof(model.ExpirationDate))
            .Should().HaveCount(1);
    }

    /// <summary>
    /// Test for <see cref="PassportModel"/> validator that should return error for passport from future
    /// </summary>
    [Fact]
    private async Task ValidateForPassportFromFutureShouldReturnError()
    {
        var model = faker.Generate();
        model.ExpirationDate = dateTimeProvider.Now().AddDays(10);
        model.IssueDate = model.ExpirationDate.AddDays(-3);

        var result = await validator.ValidateAsync(model);

        result.Errors.Where(error => error.PropertyName == nameof(model.IssueDate))
            .Should().HaveCount(1);
    }

    /// <summary>
    /// Test for <see cref="PassportModel"/> validator that should return error for passport that expired before issue
    /// </summary>
    [Fact]
    private async Task ValidateForPassportExpiredBeforeIssueShouldReturnError()
    {
        var model = faker.Generate();
        model.ExpirationDate = dateTimeProvider.Now().AddDays(10);
        model.IssueDate = model.ExpirationDate.AddDays(3);

        var result = await validator.ValidateAsync(model);

        result.Errors.Where(error => error.PropertyName == nameof(model.IssueDate))
            .Should().HaveCount(1);
    }

    /// <summary>
    /// Test for <see cref="PassportModel"/> validator that should return no errors for valid passport
    /// </summary>
    [Fact]
    private async Task ValidateForValidPassportShouldReturnNoErrors()
    {
        var model = faker.Generate();

        var result = await validator.ValidateAsync(model);

        result.Errors.Should().BeEmpty();
    }
}
