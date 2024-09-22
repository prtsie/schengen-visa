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
    public class PastVisaModelValidatorTests
    {
        private readonly static IDateTimeProvider dateTimeProvider = new TestDateTimeProvider();
        private readonly static IValidator<PastVisaModel> validator = new PastVisaModelValidator(dateTimeProvider);
        private readonly static PastVisaModelFaker faker = new(dateTimeProvider);

        /// <summary>
        /// Test for <see cref="PastVisaModel"/> validator that should return error for empty expiration date
        /// </summary>
        [Fact]
        private async Task ValidateForEmptyExpirationDateShouldReturnError()
        {
            var model = faker.GenerateValid();
            model.ExpirationDate = new();

            var result = await validator.ValidateAsync(model);

            result.Errors.Where(error => error.PropertyName == nameof(model.ExpirationDate))
                .Should().HaveCount(2); //expected error + error because of expiration date less than issue date
        }

        /// <summary>
        /// Test for <see cref="PastVisaModel"/> validator that should return error for expiration date less than issue date
        /// </summary>
        [Fact]
        private async Task ValidateForExpirationDateLessThanIssueDateShouldReturnError()
        {
            var model = faker.GenerateValid();
            model.ExpirationDate = model.IssueDate.AddDays(-4);

            var result = await validator.ValidateAsync(model);

            result.Errors.Where(error => error.PropertyName == nameof(model.ExpirationDate))
                .Should().HaveCount(1);
        }

        /// <summary>
        /// Test for <see cref="PastVisaModel"/> validator that should return error for empty issue date
        /// </summary>
        [Fact]
        private async Task ValidateForEmptyIssueDateShouldReturnError()
        {
            var model = faker.GenerateValid();
            model.IssueDate = new();

            var result = await validator.ValidateAsync(model);

            result.Errors.Where(error => error.PropertyName == nameof(model.IssueDate))
                .Should().HaveCount(1);
        }

        /// <summary>
        /// Test for <see cref="PastVisaModel"/> validator that should return error for issue date greater than current date
        /// </summary>
        [Fact]
        private async Task ValidateForIssueDateGreaterThanCurrentDateShouldReturnError()
        {
            var model = faker.GenerateValid();
            model.IssueDate = dateTimeProvider.Now().AddDays(4);

            var result = await validator.ValidateAsync(model);

            result.Errors.Where(error => error.PropertyName == nameof(model.IssueDate))
                .Should().HaveCount(1);
        }

        /// <summary>
        /// Test for <see cref="PastVisaModel"/> validator that should return error for empty name
        /// </summary>
        [Fact]
        private async Task ValidateForEmptyNameShouldReturnError()
        {
            var model = faker.GenerateValid();
            model.Name = string.Empty;

            var result = await validator.ValidateAsync(model);

            result.Errors.Where(error => error.PropertyName == nameof(model.Name))
                .Should().HaveCount(1);
        }

        /// <summary>
        /// Test for <see cref="PastVisaModel"/> validator that should return error for too long name
        /// </summary>
        [Fact]
        private async Task ValidateForTooLongNameShouldReturnError()
        {
            var model = faker.GenerateValid();
            var stringBuilder = new StringBuilder();
            stringBuilder.Append('d', ConfigurationConstraints.VisaNameLength + 1);
            model.Name = stringBuilder.ToString();

            var result = await validator.ValidateAsync(model);

            result.Errors.Where(error => error.PropertyName == nameof(model.Name))
                .Should().HaveCount(1);
        }

        /// <summary>
        /// Test for <see cref="PastVisaModel"/> validator that should return error for not valid name
        /// </summary>
        [Fact]
        private async Task ValidateForNotValidNameShouldReturnError()
        {
            var model = faker.GenerateValid();
            model.Name = "|}{%^&";

            var result = await validator.ValidateAsync(model);

            result.Errors.Where(error => error.PropertyName == nameof(model.Name))
                .Should().HaveCount(1);
        }

        /// <summary>
        /// Test for <see cref="PastVisaModel"/> validator that should return no errors for valid model
        /// </summary>
        [Fact]
        private async Task ValidateForValidShouldReturnNoErrors()
        {
            var model = faker.GenerateValid();

            var result = await validator.ValidateAsync(model);

            result.Errors.Should().BeEmpty();
        }
    }
}
