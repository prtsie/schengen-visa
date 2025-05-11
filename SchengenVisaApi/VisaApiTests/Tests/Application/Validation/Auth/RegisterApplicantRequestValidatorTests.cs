using ApplicationLayer.Services.Applicants.Models.Validation;
using ApplicationLayer.Services.AuthServices.Requests;
using ApplicationLayer.Services.AuthServices.Requests.Validation;
using Domains.ApplicantDomain;
using FluentValidation.TestHelper;
using Infrastructure.Database.Users.Repositories;
using VisaApi.Fakers.Auth;
using VisaApi.Services;
using VisaApi.Tests.Infrastructure.Database;
using Xunit;

namespace VisaApi.Tests.Application.Validation.Auth
{
    public class RegisterApplicantRequestValidatorTests
    {
        private readonly RegisterApplicantRequestValidator validator;
        private RegisterApplicantRequestFaker requestFaker;

        public RegisterApplicantRequestValidatorTests()
        {
            var context = InMemoryContextProvider.GetDbContext();
            var dateTimeProvider = new TestDateTimeProvider();
            requestFaker = new(dateTimeProvider);
            validator = new(
                dateTimeProvider,
                new NameModelValidator(),
                new RegisterRequestValidator(new UsersRepository(context, context), new AuthDataValidator()),
                new PassportModelValidator(dateTimeProvider),
                new PlaceOfWorkModelValidator()
            );
        }

        /// <summary>
        /// Validation should return no errors
        /// </summary>
        [Fact]
        public async Task ValidateShouldWork()
        {
            var request = requestFaker.Generate();

            var result = await validator.TestValidateAsync(request);

            result.ShouldNotHaveAnyValidationErrors();
        }

        /// <summary>
        /// Validation should return errors
        /// </summary>
        [Fact]
        public async Task ValidateShouldFail()
        {
            var request = new RegisterApplicantRequest
            {
                BirthDate = DateTime.Now,
                Citizenship = string.Empty,
                CitizenshipByBirth = string.Empty,
                CityOfBirth = string.Empty,
                CountryOfBirth = string.Empty,
                Gender = (Gender)123123,
                JobTitle = string.Empty,
                MaritalStatus = (MaritalStatus)123123,
            };

            var result = await validator.TestValidateAsync(request);

            var properties = request.GetType().GetProperties().Select(x => x.Name).Except([nameof(RegisterApplicantRequest.IsNonResident)]);
            foreach (var property in properties)
            {
                result.ShouldHaveValidationErrorFor(property);
            }
        }
    }
}
