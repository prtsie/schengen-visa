using ApplicationLayer.InfrastructureServicesInterfaces;
using ApplicationLayer.Services.Applicants.NeededServices;
using FluentAssertions;
using Infrastructure.Database;
using Infrastructure.Database.Applicants.Repositories;
using Infrastructure.Database.Applicants.Repositories.Exceptions;
using VisaApi.Fakers.Applicants;
using VisaApi.Fakers.Common;
using VisaApi.Services;
using Xunit;

namespace VisaApi.Database.Repositories
{
    [Collection(Collections.ContextUsingTestCollection)]
    public class ApplicantsRepositoryTests
    {
        private static UserFaker userFaker = new();
        private static ApplicantFaker applicantFaker = new(GetDateTimeProvider());

        /// <summary> Returns <see cref="IApplicantsRepository"/> </summary>
        /// <param name="context"> Database context </param>
        /// <returns>Repository</returns>
        private static IApplicantsRepository GetRepository(DbContext context)
            => new ApplicantsRepository(context, context);

        /// <summary> Returns <see cref="IDateTimeProvider"/> </summary>
        private static IDateTimeProvider GetDateTimeProvider() => new TestDateTimeProvider();

        /// <summary>
        /// Test for <see cref="IApplicantsRepository.FindByUserIdAsync"/> method that should throw exception for not existing entity
        /// </summary>
        [Fact]
        private async Task FindByUserIdForNotExistingShouldThrow()
        {
            await using var context = InMemoryContextProvider.GetDbContext();
            var repository = GetRepository(context);
            ApplicantNotFoundByUserIdException? result = null;

            try
            {
                await repository.FindByUserIdAsync(Guid.NewGuid(), CancellationToken.None);
            }
            catch (Exception e)
            {
                result = e as ApplicantNotFoundByUserIdException;
            }

            result.Should().NotBeNull();
        }

        /// <summary>
        /// Test for <see cref="IApplicantsRepository.FindByUserIdAsync"/> method that should return existing entity
        /// </summary>
        [Fact]
        private async Task FindByUserIdForExistingShouldReturnApplicant()
        {
            await using var context = InMemoryContextProvider.GetDbContext();
            var repository = GetRepository(context);
            var user = userFaker.Generate();
            var applicant = applicantFaker.Generate();
            applicant.UserId = user.Id;
            await context.AddAsync(user);
            await repository.AddAsync(applicant, CancellationToken.None);
            await context.SaveChangesAsync();

            var result = await repository.FindByUserIdAsync(user.Id, CancellationToken.None);

            result.Should().BeEquivalentTo(applicant);
        }

        /// <summary>
        /// Test for <see cref="IApplicantsRepository.GetApplicantIdByUserId"/> method that should throw exception for not existing entity
        /// </summary>
        [Fact]
        private async Task GetApplicantIdByUserIdForNotExistingShouldThrow()
        {
            await using var context = InMemoryContextProvider.GetDbContext();
            var repository = GetRepository(context);
            ApplicantNotFoundByUserIdException? result = null;

            try
            {
                await repository.GetApplicantIdByUserId(Guid.NewGuid(), CancellationToken.None);
            }
            catch (Exception e)
            {
                result = e as ApplicantNotFoundByUserIdException;
            }

            result.Should().NotBeNull();
        }

        /// <summary>
        /// Test for <see cref="IApplicantsRepository.GetApplicantIdByUserId"/> method that should return existing entity's identifier
        /// </summary>
        [Fact]
        private async Task GetApplicantIdByUserIdForExistingShouldReturnApplicant()
        {
            await using var context = InMemoryContextProvider.GetDbContext();
            var repository = GetRepository(context);
            var user = userFaker.Generate();
            var applicant = applicantFaker.Generate();
            applicant.UserId = user.Id;
            await context.AddAsync(user);
            await repository.AddAsync(applicant, CancellationToken.None);
            await context.SaveChangesAsync();

            var result = await repository.GetApplicantIdByUserId(user.Id, CancellationToken.None);

            result.Should().Be(applicant.Id);
        }

        /// <summary>
        /// Test for <see cref="IApplicantsRepository.IsApplicantNonResidentByUserId"/> method that should throw exception for not existing entity
        /// </summary>
        [Fact]
        private async Task IsApplicantNonResidentByUserIdForNotExistingShouldThrow()
        {
            await using var context = InMemoryContextProvider.GetDbContext();
            var repository = GetRepository(context);
            ApplicantNotFoundByUserIdException? result = null;

            try
            {
                await repository.IsApplicantNonResidentByUserId(Guid.NewGuid(), CancellationToken.None);
            }
            catch (Exception e)
            {
                result = e as ApplicantNotFoundByUserIdException;
            }

            result.Should().NotBeNull();
        }

        /// <summary>
        /// Test for <see cref="IApplicantsRepository.IsApplicantNonResidentByUserId"/> method that should return existing entity's IsNonResident property
        /// </summary>
        [Fact]
        private async Task IsApplicantNonResidentByUserIdForExistingShouldReturnApplicant()
        {
            await using var context = InMemoryContextProvider.GetDbContext();
            var repository = GetRepository(context);
            var user = userFaker.Generate();
            var applicant = new ApplicantFaker(GetDateTimeProvider()).Generate();
            applicant.UserId = user.Id;
            await context.AddAsync(user);
            await repository.AddAsync(applicant, CancellationToken.None);
            await context.SaveChangesAsync();

            var result = await repository.IsApplicantNonResidentByUserId(user.Id, CancellationToken.None);

            result.Should().Be(applicant.IsNonResident);
        }
    }
}
