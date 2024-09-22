using System.Text;
using ApplicationLayer.Services.Applicants.Models;
using ApplicationLayer.Services.Applicants.Models.Validation;
using Domains;
using FluentAssertions;
using FluentValidation;
using VisaApi.Fakers.Applicants.Requests;
using Xunit;

namespace VisaApi.Tests.Application.Validation.Applicants
{
    public class PlaceOfWorkModelValidatorTests
    {
        private static IValidator<PlaceOfWorkModel> validator = new PlaceOfWorkModelValidator();
        private static PlaceOfWorkModelFaker faker = new();

        /// <summary>
        /// Test for <see cref="PlaceOfWorkModel"/> validator that should return error for empty phone num
        /// </summary>
        [Fact]
        private async Task ValidateForEmptyPhoneNumShouldReturnError()
        {
            var model = faker.Generate();
            model.PhoneNum = string.Empty;

            var result = await validator.ValidateAsync(model);

            result.Errors.Where(error => error.PropertyName == nameof(model.PhoneNum))
                .Should().NotBeEmpty();
        }

        /// <summary>
        /// Test for <see cref="PlaceOfWorkModel"/> validator that should return error for long phone num
        /// </summary>
        [Fact]
        private async Task ValidateForLongPhoneNumShouldReturnError()
        {
            var model = faker.Generate();
            var stringBuilder = new StringBuilder();
            stringBuilder.Append('8', ConfigurationConstraints.PhoneNumberLength + 1);
            model.PhoneNum = stringBuilder.ToString();

            var result = await validator.ValidateAsync(model);

            result.Errors.Where(error => error.PropertyName == nameof(model.PhoneNum))
                .Should().NotBeEmpty();
        }

        /// <summary>
        /// Test for <see cref="PlaceOfWorkModel"/> validator that should return error for short phone num
        /// </summary>
        [Fact]
        private async Task ValidateForShortPhoneNumShouldReturnError()
        {
            var model = faker.Generate();
            var stringBuilder = new StringBuilder();
            stringBuilder.Append('8', ConfigurationConstraints.PhoneNumberMinLength - 1);
            model.PhoneNum = stringBuilder.ToString();

            var result = await validator.ValidateAsync(model);

            result.Errors.Where(error => error.PropertyName == nameof(model.PhoneNum))
                .Should()
                .HaveCount(1);
        }

        /// <summary>
        /// Test for <see cref="PlaceOfWorkModel"/> validator that should return error for not valid phone num
        /// </summary>
        [Fact]
        private async Task ValidateForNotValidPhoneNumShouldReturnError()
        {
            var model = faker.Generate();
            var stringBuilder = new StringBuilder();
            stringBuilder.Append('a', ConfigurationConstraints.PhoneNumberMinLength);
            model.PhoneNum = stringBuilder.ToString();

            var result = await validator.ValidateAsync(model);

            result.Errors.Where(error => error.PropertyName == nameof(model.PhoneNum))
                .Should()
                .HaveCount(1);
        }

        /// <summary>
        /// Test for <see cref="PlaceOfWorkModel"/> validator that should throw exception for null address
        /// </summary>
        [Fact]
        private async Task ValidateForEmptyAddressShouldThrow()
        {
            var model = faker.Generate();
            model.Address = null!;
            NullReferenceException? result = null;

            try
            {
                await validator.ValidateAsync(model);
            }
            catch (Exception e)
            {
                result = e as NullReferenceException;
            }

            result.Should().NotBeNull();
        }

        /// <summary>
        /// Test for <see cref="PlaceOfWorkModel"/> validator that should return error for empty Country
        /// </summary>
        [Fact]
        private async Task ValidateForEmptyCountryShouldReturnError()
        {
            var model = faker.Generate();
            model.Address.Country = "";

            var result = await validator.ValidateAsync(model);

            result.Errors.Where(error => error.PropertyName == "Address.Country")
                .Should().HaveCount(1);
        }

        /// <summary>
        /// Test for <see cref="PlaceOfWorkModel"/> validator that should return error for too long Country
        /// </summary>
        [Fact]
        private async Task ValidateForLongCountryShouldReturnError()
        {
            var model = faker.Generate();
            var stringBuilder = new StringBuilder();
            stringBuilder.Append('a', ConfigurationConstraints.CountryNameLength + 1);
            model.Address.Country = stringBuilder.ToString();

            var result = await validator.ValidateAsync(model);

            result.Errors.Where(error => error.PropertyName == "Address.Country")
                .Should().HaveCount(1);
        }

        /// <summary>
        /// Test for <see cref="PlaceOfWorkModel"/> validator that should return error for not valid Country
        /// </summary>
        [Fact]
        private async Task ValidateForNotValidCountryShouldReturnError()
        {
            var model = faker.Generate();
            model.Address.Country = "|&%";

            var result = await validator.ValidateAsync(model);

            result.Errors.Where(error => error.PropertyName == "Address.Country")
                .Should().HaveCount(1);
        }

        /// <summary>
        /// Test for <see cref="PlaceOfWorkModel"/> validator that should return error for empty City
        /// </summary>
        [Fact]
        private async Task ValidateForEmptyCityShouldReturnError()
        {
            var model = faker.Generate();
            model.Address.City = "";

            var result = await validator.ValidateAsync(model);

            result.Errors.Where(error => error.PropertyName == "Address.City")
                .Should().HaveCount(1);
        }

        /// <summary>
        /// Test for <see cref="PlaceOfWorkModel"/> validator that should return error for too long city
        /// </summary>
        [Fact]
        private async Task ValidateForLongCityShouldReturnError()
        {
            var model = faker.Generate();
            var stringBuilder = new StringBuilder();
            stringBuilder.Append('a', ConfigurationConstraints.CityNameLength + 1);
            model.Address.City = stringBuilder.ToString();

            var result = await validator.ValidateAsync(model);

            result.Errors.Where(error => error.PropertyName == "Address.City")
                .Should().HaveCount(1);
        }

        /// <summary>
        /// Test for <see cref="PlaceOfWorkModel"/> validator that should return error for not valid city
        /// </summary>
        [Fact]
        private async Task ValidateForNotValidCityShouldReturnError()
        {
            var model = faker.Generate();
            model.Address.City = "|&%";

            var result = await validator.ValidateAsync(model);

            result.Errors.Where(error => error.PropertyName == "Address.City")
                .Should().HaveCount(1);
        }

        /// <summary>
        /// Test for <see cref="PlaceOfWorkModel"/> validator that should return error for empty street
        /// </summary>
        [Fact]
        private async Task ValidateForEmptyStreetShouldReturnError()
        {
            var model = faker.Generate();
            model.Address.Street = "";

            var result = await validator.ValidateAsync(model);

            result.Errors.Where(error => error.PropertyName == "Address.Street")
                .Should().HaveCount(1);
        }

        /// <summary>
        /// Test for <see cref="PlaceOfWorkModel"/> validator that should return error for too long street
        /// </summary>
        [Fact]
        private async Task ValidateForLongStreetShouldReturnError()
        {
            var model = faker.Generate();
            var stringBuilder = new StringBuilder();
            stringBuilder.Append('a', ConfigurationConstraints.StreetNameLength + 1);
            model.Address.Street = stringBuilder.ToString();

            var result = await validator.ValidateAsync(model);

            result.Errors.Where(error => error.PropertyName == "Address.Street")
                .Should().HaveCount(1);
        }

        /// <summary>
        /// Test for <see cref="PlaceOfWorkModel"/> validator that should return error for not valid street
        /// </summary>
        [Fact]
        private async Task ValidateForNotValidStreetShouldReturnError()
        {
            var model = faker.Generate();
            model.Address.Street = "|&%";

            var result = await validator.ValidateAsync(model);

            result.Errors.Where(error => error.PropertyName == "Address.Street")
                .Should().HaveCount(1);
        }

        /// <summary>
        /// Test for <see cref="PlaceOfWorkModel"/> validator that should return error for empty building        /// </summary>
        [Fact]
        private async Task ValidateForEmptyBuildingShouldReturnError()
        {
            var model = faker.Generate();
            model.Address.Building = "";

            var result = await validator.ValidateAsync(model);

            result.Errors.Where(error => error.PropertyName == "Address.Building")
                .Should().HaveCount(1);
        }

        /// <summary>
        /// Test for <see cref="PlaceOfWorkModel"/> validator that should return error for too long building
        /// </summary>
        [Fact]
        private async Task ValidateForLongBuildingShouldReturnError()
        {
            var model = faker.Generate();
            var stringBuilder = new StringBuilder();
            stringBuilder.Append('a', ConfigurationConstraints.BuildingNumberLength + 1);
            model.Address.Building = stringBuilder.ToString();

            var result = await validator.ValidateAsync(model);

            result.Errors.Where(error => error.PropertyName == "Address.Building")
                .Should().HaveCount(1);
        }

        /// <summary>
        /// Test for <see cref="PlaceOfWorkModel"/> validator that should return error for not valid building
        /// </summary>
        [Fact]
        private async Task ValidateForNotValidBuildingShouldReturnError()
        {
            var model = faker.Generate();
            model.Address.Building = "|&%";

            var result = await validator.ValidateAsync(model);

            result.Errors.Where(error => error.PropertyName == "Address.Building")
                .Should().HaveCount(1);
        }

        /// <summary>
        /// Test for <see cref="PlaceOfWorkModel"/> validator that should return error for empty name
        /// </summary>
        [Fact]
        private async Task ValidateForEmptyNameShouldReturnError()
        {
            var model = faker.Generate();
            model.Name = "";

            var result = await validator.ValidateAsync(model);

            result.Errors.Where(error => error.PropertyName == nameof(model.Name))
                .Should().HaveCount(1);
        }

        /// <summary>
        /// Test for <see cref="PlaceOfWorkModel"/> validator that should return error for too long name
        /// </summary>
        [Fact]
        private async Task ValidateForTooLongNameShouldReturnError()
        {
            var model = faker.Generate();
            var stringBuilder = new StringBuilder();
            stringBuilder.Append('g', ConfigurationConstraints.PlaceOfWorkNameLength + 1);
            model.Name = stringBuilder.ToString();

            var result = await validator.ValidateAsync(model);

            result.Errors.Where(error => error.PropertyName == nameof(model.Name))
                .Should().HaveCount(1);
        }

        /// <summary>
        /// Test for <see cref="PlaceOfWorkModel"/> validator that should return error for not valid name
        /// </summary>
        [Fact]
        private async Task ValidateForNotValidNameShouldReturnError()
        {
            var model = faker.Generate();
            model.Name = "@$%&|";

            var result = await validator.ValidateAsync(model);

            result.Errors.Where(error => error.PropertyName == nameof(model.Name))
                .Should().HaveCount(1);
        }

        /// <summary>
        /// Test for <see cref="PlaceOfWorkModel"/> validator that should return no errors for valid model
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
