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
    public class ReentryPermitModelValidatorTests
    {
        private readonly static IDateTimeProvider dateTimeProvider = new TestDateTimeProvider();
        private readonly static IValidator<ReentryPermitModel> validator = new ReentryPermitModelValidator(dateTimeProvider);
        private readonly static ReentryPermitModelFaker faker = new(dateTimeProvider);

        /// <summary>
        /// Test for <see cref="ReentryPermitModel"/> validator that should return error for empty expiration date
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
        /// Test for <see cref="ReentryPermitModel"/> validator that should return error for expiration date less than current date
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
        /// Test for <see cref="ReentryPermitModel"/> validator that should return error for empty number
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
        /// Test for <see cref="ReentryPermitModel"/> validator that should return error for not valid number
        /// </summary>
        [Fact]
        private async Task ValidateForNotValidNumberShouldReturnError()
        {
            var model = faker.Generate();
            model.Number = "}{)(*&*^%#!#!:";

            var result = await validator.ValidateAsync(model);

            result.Errors.Where(error => error.PropertyName == nameof(model.Number))
                .Should().HaveCount(1);
        }

        /// <summary>
        /// Test for <see cref="ReentryPermitModel"/> validator that should return error for too long number
        /// </summary>
        [Fact]
        private async Task ValidateForTooLongNumberShouldReturnError()
        {
            var model = faker.Generate();
            var stringBuilder = new StringBuilder();
            stringBuilder.Append('g', ConfigurationConstraints.ReentryPermitNumberLength + 1);
            model.Number = stringBuilder.ToString();

            var result = await validator.ValidateAsync(model);

            result.Errors.Where(error => error.PropertyName == nameof(model.Number))
                .Should().HaveCount(1);
        }

        /// <summary>
        /// Test for <see cref="ReentryPermitModel"/> validator that should return no errors for valid model
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
