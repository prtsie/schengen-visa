using System.Text;
using ApplicationLayer.InfrastructureServicesInterfaces;
using ApplicationLayer.Services.VisaApplications.Models;
using ApplicationLayer.Services.VisaApplications.Models.Validation;
using Domains;
using FluentAssertions;
using FluentValidation;
using VisaApi.Fakers.VisaApplications.Requests;
using VisaApi.Services;
using Xunit;

namespace VisaApi.Tests.Application.Validation.VisaApplications
{
    public class PermissionToDestCountryModelValidatorTests
    {
        private readonly static IDateTimeProvider dateTimeProvider = new TestDateTimeProvider();
        private readonly static IValidator<PermissionToDestCountryModel> validator = new PermissionToDestCountryModelValidator(dateTimeProvider);
        private readonly static PermissionToDestCountryModelFaker faker = new(dateTimeProvider);

        /// <summary>
        /// Test for <see cref="PermissionToDestCountryModel"/> validator that should return error for empty expiration date
        /// </summary>
        [Fact]
        private async Task ValidateForEmptyExpirationDateShouldReturnError()
        {
            var model = faker.Generate();
            model.ExpirationDate = new();

            var result = await validator.ValidateAsync(model);

            result.Errors.Where(error => error.PropertyName == nameof(model.ExpirationDate))
                .Should().HaveCount(2); //expected error + error because of expired
        }

        /// <summary>
        /// Test for <see cref="PermissionToDestCountryModel"/> validator that should return error for expiration date less than current date
        /// </summary>
        [Fact]
        private async Task ValidateForExpirationDateLessThanCurrentDateShouldReturnError()
        {
            var model = faker.Generate();
            model.ExpirationDate = dateTimeProvider.Now().AddDays(-1);

            var result = await validator.ValidateAsync(model);

            result.Errors.Where(error => error.PropertyName == nameof(model.ExpirationDate))
                .Should().HaveCount(1);
        }

        /// <summary>
        /// Test for <see cref="PermissionToDestCountryModel"/> validator that should return error for empty issuer
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
        /// Test for <see cref="PermissionToDestCountryModel"/> validator that should return error for not valid issuer
        /// </summary>
        [Fact]
        private async Task ValidateForNotValidIssuerShouldReturnError()
        {
            var model = faker.Generate();
            model.Issuer = "}{)(*&*^%#!#!:";

            var result = await validator.ValidateAsync(model);

            result.Errors.Where(error => error.PropertyName == nameof(model.Issuer))
                .Should().HaveCount(1);
        }

        /// <summary>
        /// Test for <see cref="PermissionToDestCountryModel"/> validator that should return error for too long issuer
        /// </summary>
        [Fact]
        private async Task ValidateForTooLongIssuerShouldReturnError()
        {
            var model = faker.Generate();
            var stringBuilder = new StringBuilder();
            stringBuilder.Append('g', ConfigurationConstraints.IssuerNameLength + 1);
            model.Issuer = stringBuilder.ToString();

            var result = await validator.ValidateAsync(model);

            result.Errors.Where(error => error.PropertyName == nameof(model.Issuer))
                .Should().HaveCount(1);
        }

        /// <summary>
        /// Test for <see cref="PermissionToDestCountryModel"/> validator that should return no errors for valid model
        /// </summary>
        [Fact]
        private async Task ValidateForValidShouldReturnNoErrors()
        {
            var model = faker.Generate();

            var result = await validator.ValidateAsync(model);

            result.Errors.Should().BeEmpty();
        }
    }
}
