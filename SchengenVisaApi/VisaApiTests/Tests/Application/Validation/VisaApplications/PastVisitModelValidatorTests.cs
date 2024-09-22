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
    public class PastVisitModelValidatorTests
    {
        private readonly static IDateTimeProvider dateTimeProvider = new TestDateTimeProvider();
        private readonly static IValidator<PastVisitModel> validator = new PastVisitModelValidator(dateTimeProvider);
        private readonly static PastVisitModelFaker faker = new(dateTimeProvider);

        /// <summary>
        /// Test for <see cref="PastVisitModel"/> validator that should return error for empty start date
        /// </summary>
        [Fact]
        private async Task ValidateForEmptyStartDateShouldReturnError()
        {
            var model = faker.GenerateValid();
            model.StartDate = new();

            var result = await validator.ValidateAsync(model);

            result.Errors.Where(error => error.PropertyName == nameof(model.StartDate))
                .Should().HaveCount(1);
        }

        /// <summary>
        /// Test for <see cref="PastVisitModel"/> validator that should return error for start date greater than end date
        /// </summary>
        [Fact]
        private async Task ValidateForStartDateGreaterThanEndDateShouldReturnError()
        {
            var model = faker.GenerateValid();
            model.EndDate = dateTimeProvider.Now().AddDays(-10);
            model.StartDate = model.EndDate.AddDays(4);

            var result = await validator.ValidateAsync(model);

            result.Errors.Where(error => error.PropertyName == nameof(model.StartDate))
                .Should().HaveCount(1);
        }

        /// <summary>
        /// Test for <see cref="PastVisitModel"/> validator that should return error for empty end date
        /// </summary>
        [Fact]
        private async Task ValidateForEmptyEndDateShouldReturnError()
        {
            var model = faker.GenerateValid();
            model.EndDate = new();

            var result = await validator.ValidateAsync(model);

            result.Errors.Where(error => error.PropertyName == nameof(model.EndDate))
                .Should().HaveCount(1); //expected error + error because of end date less than start date
        }

        /// <summary>
        /// Test for <see cref="PastVisitModel"/> validator that should return error for start date greater than current date
        /// </summary>
        [Fact]
        private async Task ValidateForStartDateGreaterThanCurrentDateShouldReturnError()
        {
            var model = faker.GenerateValid();
            model.StartDate = dateTimeProvider.Now().AddDays(4);
            model.EndDate = model.StartDate.AddDays(4);

            var result = await validator.ValidateAsync(model);

            result.Errors.Where(error => error.PropertyName == nameof(model.StartDate))
                .Should().HaveCount(1);
        }

        /// <summary>
        /// Test for <see cref="PastVisitModel"/> validator that should return error for empty destination Country
        /// </summary>
        [Fact]
        private async Task ValidateForEmptyDestinationCountryShouldReturnError()
        {
            var model = faker.GenerateValid();
            model.DestinationCountry = string.Empty;

            var result = await validator.ValidateAsync(model);

            result.Errors.Where(error => error.PropertyName == nameof(model.DestinationCountry))
                .Should().HaveCount(1);
        }

        /// <summary>
        /// Test for <see cref="PastVisitModel"/> validator that should return error for too long destination Country
        /// </summary>
        [Fact]
        private async Task ValidateForTooLongDestinationCountryShouldReturnError()
        {
            var model = faker.GenerateValid();
            var stringBuilder = new StringBuilder();
            stringBuilder.Append('d', ConfigurationConstraints.CountryNameLength + 1);
            model.DestinationCountry = stringBuilder.ToString();

            var result = await validator.ValidateAsync(model);

            result.Errors.Where(error => error.PropertyName == nameof(model.DestinationCountry))
                .Should().HaveCount(1);
        }

        /// <summary>
        /// Test for <see cref="PastVisitModel"/> validator that should return error for not valid destination Country
        /// </summary>
        [Fact]
        private async Task ValidateForNotValidDestinationCountryShouldReturnError()
        {
            var model = faker.GenerateValid();
            model.DestinationCountry = "|}{%^&";

            var result = await validator.ValidateAsync(model);

            result.Errors.Where(error => error.PropertyName == nameof(model.DestinationCountry))
                .Should().HaveCount(1);
        }

        /// <summary>
        /// Test for <see cref="PastVisitModel"/> validator that should return no errors for valid model
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
