﻿using ApplicationLayer.Services.Users.Requests;
using ApplicationLayer.Services.Users.Requests.Validation;
using FluentAssertions;
using FluentValidation;
using VisaApi.Fakers.Users.Requests;
using Xunit;

namespace VisaApi.Tests.Application.Validation.Users
{
    public class ChangeUserAuthDataRequestValidationTests
    {
        private readonly static IValidator<ChangeUserAuthDataRequest> validator = new ChangeUserAuthDataRequestValidator();
        private readonly static ChangeUserAuthDataRequestFaker faker = new();

        /// <summary>
        /// Test for <see cref="ChangeUserAuthDataRequest"/> validator that should return validation error for empty auth data
        /// </summary>
        [Fact]
        private async Task ValidateForEmptyAuthDataShouldReturnError()
        {
            var request = faker.Generate();
            request.NewAuthData = null!;
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
    }
}
