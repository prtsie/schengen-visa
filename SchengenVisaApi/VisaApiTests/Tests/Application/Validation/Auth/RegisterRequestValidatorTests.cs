using ApplicationLayer.InfrastructureServicesInterfaces;
using ApplicationLayer.Services.AuthServices.Common;
using ApplicationLayer.Services.AuthServices.Requests;
using ApplicationLayer.Services.AuthServices.Requests.Validation;
using FluentAssertions;
using FluentValidation;
using Infrastructure.Database;
using Infrastructure.Database.Users.Repositories;
using VisaApi.Fakers.Auth;
using VisaApi.Fakers.Users;
using VisaApi.Tests.Infrastructure.Database;
using Xunit;

namespace VisaApi.Tests.Application.Validation.Auth
{
    [Collection(Collections.ContextUsingTestCollection)]
    public class RegisterRequestValidatorTests
    {
        private readonly static IValidator<AuthData> authDataValidator = new AuthDataValidator();
        private readonly static RegisterRequestFaker requestFaker = new();
        private readonly static UserFaker userFaker = new();

        /// <summary>
        /// Creates validator from context
        /// </summary>
        /// <param name="context">db context</param>
        /// <returns>RegisterRequest validator</returns>
        private static IValidator<RegisterRequest> GetValidator(DbContext context)
        {
            var repository = new UsersRepository(context, context);
            return new RegisterRequestValidator(repository, authDataValidator);
        }

        /// <summary>
        /// Test for <see cref="RegisterRequest"/> validator that should throw for empty auth data
        /// </summary>
        [Fact]
        private async Task ValidateForEmptyAuthDataShouldThrow()
        {
            var context = InMemoryContextProvider.GetDbContext();
            var validator = GetValidator(context);
            var request = requestFaker.Generate();
            request.AuthData = null!;
            NullReferenceException? result = null;

            try
            {
                await validator.ValidateAsync(request);
            }
            catch (Exception e)
            {
                result = e as NullReferenceException;
            }

            result.Should().NotBeNull();
        }

        /// <summary>
        /// Test for <see cref="RegisterRequest"/> validator that should return error for used email
        /// </summary>
        [Fact]
        private async Task ValidateForUsedEmailShouldReturnError()
        {
            var context = InMemoryContextProvider.GetDbContext();
            var validator = GetValidator(context);
            var user = userFaker.Generate();
            await context.AddAsync(user);
            await context.SaveChangesAsync();
            var request = requestFaker.Generate();
            request.AuthData.Email = user.Email;

            var result = await validator.ValidateAsync(request);

            result.Errors.Should()
                .Contain(error => error.PropertyName == nameof(request.AuthData))
                .And.HaveCount(1);
        }

        /// <summary>
        /// Test for <see cref="RegisterRequest"/> validator that should return o errors for valid requests
        /// </summary>
        [Fact]
        private async Task ValidateForValidRequestShouldReturnNoErrors()
        {
            var context = InMemoryContextProvider.GetDbContext();
            var validator = GetValidator(context);
            var request = requestFaker.Generate();

            var result = await validator.ValidateAsync(request);

            result.Errors.Should().BeEmpty();
        }
    }
}
