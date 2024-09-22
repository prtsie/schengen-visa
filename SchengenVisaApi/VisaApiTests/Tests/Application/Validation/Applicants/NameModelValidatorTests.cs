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
    public class NameModelValidatorTests
    {
        private static IValidator<NameModel> validator = new NameModelValidator();
        private static NameModelFaker faker = new();

        /// <summary>
        /// Test for <see cref="NameModel"/> validator that should throw for empty first name
        /// </summary>
        [Fact]
        private async Task ValidateForEmptyFirstNameShouldThrow()
        {
            var name = faker.Generate();
            name.FirstName = null!;

            var result = await validator.ValidateAsync(name);

            result.Errors.Where(error => error.PropertyName == nameof(name.FirstName))
                .Should().HaveCount(1);
        }

        /// <summary>
        /// Test for <see cref="NameModel"/> validator that should throw for empty surname
        /// </summary>
        [Fact]
        private async Task ValidateForEmptySurnameShouldThrow()
        {
            var name = faker.Generate();
            name.Surname = null!;

            var result = await validator.ValidateAsync(name);

            result.Errors.Where(error => error.PropertyName == nameof(name.Surname))
                .Should().HaveCount(1);
        }

        /// <summary>
        /// Test for <see cref="NameModel"/> validator that should return no errors for empty patronymic
        /// </summary>
        [Fact]
        private async Task ValidateForEmptyPatronymicShouldReturnNoErrors()
        {
            var name = faker.Generate();
            name.Patronymic = null;

            var result = await validator.ValidateAsync(name);

            result.Errors.Where(error => error.PropertyName == nameof(name.Patronymic))
                .Should().BeEmpty();
        }

        /// <summary>
        /// Test for <see cref="NameModel"/> validator that should return error for too long first name
        /// </summary>
        [Fact]
        private async Task ValidateForLongFirstNameShouldReturnError()
        {
            var name = faker.Generate();
            var stringBuilder = new StringBuilder();
            stringBuilder.Append('h', ConfigurationConstraints.NameLength + 1);
            name.FirstName = stringBuilder.ToString();

            var result = await validator.ValidateAsync(name);

            result.Errors.Where(error => error.PropertyName == nameof(name.FirstName))
                .Should().HaveCount(1);
        }

        /// <summary>
        /// Test for <see cref="NameModel"/> validator that should return error for too long surname
        /// </summary>
        [Fact]
        private async Task ValidateForLongSurnameShouldReturnError()
        {
            var name = faker.Generate();
            var stringBuilder = new StringBuilder();
            stringBuilder.Append('h', ConfigurationConstraints.NameLength + 1);
            name.Surname = stringBuilder.ToString();

            var result = await validator.ValidateAsync(name);

            result.Errors.Where(error => error.PropertyName == nameof(name.Surname))
                .Should().HaveCount(1);
        }

        /// <summary>
        /// Test for <see cref="NameModel"/> validator that should return error for too long patronymic
        /// </summary>
        [Fact]
        private async Task ValidateForLongPatronymicShouldReturnError()
        {
            var name = faker.Generate();
            var stringBuilder = new StringBuilder();
            stringBuilder.Append('h', ConfigurationConstraints.NameLength + 1);
            name.Patronymic = stringBuilder.ToString();

            var result = await validator.ValidateAsync(name);

            result.Errors.Where(error => error.PropertyName == nameof(name.Patronymic))
                .Should().HaveCount(1);
        }

        /// <summary>
        /// Test for <see cref="NameModel"/> validator that should return error for not valid firstname
        /// </summary>
        [Fact]
        private async Task ValidateForNotValidFirstNameShouldReturnError()
        {
            var name = faker.Generate();
            name.FirstName = "&&7!**|";

            var result = await validator.ValidateAsync(name);

            result.Errors.Where(error => error.PropertyName == nameof(name.FirstName))
                .Should().HaveCount(1);
        }

        /// <summary>
        /// Test for <see cref="NameModel"/> validator that should return error for not valid surname
        /// </summary>
        [Fact]
        private async Task ValidateForNotValidSurnameShouldReturnError()
        {
            var name = faker.Generate();
            name.Surname = "&&7!**|";

            var result = await validator.ValidateAsync(name);

            result.Errors.Where(error => error.PropertyName == nameof(name.Surname))
                .Should().HaveCount(1);
        }

        /// <summary>
        /// Test for <see cref="NameModel"/> validator that should return error for not valid patronymic
        /// </summary>
        [Fact]
        private async Task ValidateForNotValidPatronymicShouldReturnError()
        {
            var name = faker.Generate();
            name.Patronymic = "&&7!**|";

            var result = await validator.ValidateAsync(name);

            result.Errors.Where(error => error.PropertyName == nameof(name.Patronymic))
                .Should().HaveCount(1);
        }

        /// <summary>
        /// Test for <see cref="NameModel"/> validator that should return no errors for valid name
        /// </summary>
        [Fact]
        private async Task ValidateForValidNameShouldReturnNoErrors()
        {
            var name = faker.Generate();

            var result = await validator.ValidateAsync(name);

            result.Errors.Should().BeEmpty();
        }
    }
}
