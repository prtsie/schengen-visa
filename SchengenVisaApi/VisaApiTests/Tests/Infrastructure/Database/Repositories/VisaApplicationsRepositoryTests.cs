using ApplicationLayer.InfrastructureServicesInterfaces;
using ApplicationLayer.Services.VisaApplications.NeededServices;
using Domains.VisaApplicationDomain;
using FluentAssertions;
using Infrastructure.Database;
using Infrastructure.Database.VisaApplications.Repositories;
using Infrastructure.Database.VisaApplications.Repositories.Exceptions;
using VisaApi.Fakers.Applicants;
using VisaApi.Fakers.Common;
using VisaApi.Fakers.VisaApplications;
using VisaApi.Services;
using Xunit;

namespace VisaApi.Tests.Infrastructure.Database.Repositories
{
    [Collection(Collections.ContextUsingTestCollection)]
    public class VisaApplicationsRepositoryTests
    {
        private UserFaker userFaker = new();
        private ApplicantFaker applicantFaker = new(GetDateTimeProvider());
        private VisaApplicationFaker applicationFaker = new(GetDateTimeProvider());

        /// <summary> Returns <see cref="IVisaApplicationsRepository"/> </summary>
        /// <param name="context"> Database context </param>
        /// <returns>Repository</returns>
        private static IVisaApplicationsRepository GetRepository(DbContext context)
            => new VisaApplicationsRepository(context, context);

        /// <summary> Returns <see cref="IDateTimeProvider"/> </summary>
        private static IDateTimeProvider GetDateTimeProvider() => new TestDateTimeProvider();

        /// <summary>
        /// Test for <see cref="IVisaApplicationsRepository.GetOfApplicantAsync"/> method that should return empty if no applications added
        /// </summary>
        [Fact]
        private async Task GetOfApplicantForEmptyShouldReturnEmpty()
        {
            await using var context = InMemoryContextProvider.GetDbContext();
            var repository = GetRepository(context);
            var user = userFaker.Generate();
            var applicant = applicantFaker.Generate();
            applicant.UserId = user.Id;
            await context.AddAsync(user);
            await context.AddAsync(applicant);
            await context.SaveChangesAsync();

            var result = await repository.GetOfApplicantAsync(applicant.Id, CancellationToken.None);

            result.Should().BeEmpty();
        }

        /// <summary>
        /// Test for <see cref="IVisaApplicationsRepository.GetOfApplicantAsync"/> method that should return added entities
        /// </summary>
        [Fact]
        private async Task GetOfApplicantForExistingShouldReturnEntities()
        {
            await using var context = InMemoryContextProvider.GetDbContext();
            var repository = GetRepository(context);
            var user = userFaker.Generate();
            var applicant = applicantFaker.Generate();
            applicant.UserId = user.Id;
            await context.AddAsync(user);
            await context.AddAsync(applicant);
            var applications = new List<VisaApplication>();
            for (var i = 0; i < 5; i++)
            {
                var application = applicationFaker.GenerateValid(applicant);
                applications.Add(application);
                await context.AddAsync(application);
            }

            await context.SaveChangesAsync();

            var result = await repository.GetOfApplicantAsync(applicant.Id, CancellationToken.None);

            result.Should().Contain(applications).And.HaveSameCount(applications);
        }

        /// <summary>
        /// Test for <see cref="IVisaApplicationsRepository.GetByApplicantAndApplicationIdAsync"/> method that should throw exception for not existing entities
        /// </summary>
        [Fact]
        private async Task GetApplicantIdByUserIdForNotExistingShouldThrow()
        {
            await using var context = InMemoryContextProvider.GetDbContext();
            var repository = GetRepository(context);
            ApplicationNotFoundByApplicantAndApplicationIdException? result = null;

            await context.SaveChangesAsync();

            try
            {
                await repository.GetByApplicantAndApplicationIdAsync(Guid.NewGuid(), Guid.NewGuid(), CancellationToken.None);
            }
            catch (Exception e)
            {
                result = e as ApplicationNotFoundByApplicantAndApplicationIdException;
            }

            result.Should().NotBeNull();
        }

        /// <summary>
        /// Test for <see cref="IVisaApplicationsRepository.GetByApplicantAndApplicationIdAsync"/> method that should throw exception for not existing applicant
        /// </summary>
        [Fact]
        private async Task GetApplicantIdByUserIdForNotExistingApplicantShouldThrow()
        {
            await using var context = InMemoryContextProvider.GetDbContext();
            var repository = GetRepository(context);
            var user = userFaker.Generate();
            var applicant = applicantFaker.Generate();
            applicant.UserId = user.Id;
            var application = applicationFaker.GenerateValid(applicant);
            await context.AddAsync(user);
            await context.AddAsync(applicant);
            await context.AddAsync(application);
            ApplicationNotFoundByApplicantAndApplicationIdException? result = null;

            await context.SaveChangesAsync();

            try
            {
                await repository.GetByApplicantAndApplicationIdAsync(Guid.NewGuid(), application.Id, CancellationToken.None);
            }
            catch (Exception e)
            {
                result = e as ApplicationNotFoundByApplicantAndApplicationIdException;
            }

            result.Should().NotBeNull();
        }

        /// <summary>
        /// Test for <see cref="IVisaApplicationsRepository.GetByApplicantAndApplicationIdAsync"/> method that should throw exception for not existing application
        /// </summary>
        [Fact]
        private async Task GetApplicantIdByUserIdForNotExistingApplicationShouldThrow()
        {
            await using var context = InMemoryContextProvider.GetDbContext();
            var repository = GetRepository(context);
            var user = userFaker.Generate();
            var applicant = applicantFaker.Generate();
            applicant.UserId = user.Id;
            await context.AddAsync(user);
            await context.AddAsync(applicant);
            ApplicationNotFoundByApplicantAndApplicationIdException? result = null;

            await context.SaveChangesAsync();

            try
            {
                await repository.GetByApplicantAndApplicationIdAsync(applicant.Id, Guid.NewGuid(), CancellationToken.None);
            }
            catch (Exception e)
            {
                result = e as ApplicationNotFoundByApplicantAndApplicationIdException;
            }

            result.Should().NotBeNull();
        }

        /// <summary>
        /// Test for <see cref="IVisaApplicationsRepository.GetByApplicantAndApplicationIdAsync"/> method
        /// that should throw exception for not accessible application
        /// </summary>
        [Fact]
        private async Task GetApplicantIdByUserIdForNotAccessibleApplicationShouldThrow()
        {
            await using var context = InMemoryContextProvider.GetDbContext();
            var repository = GetRepository(context);
            var user = userFaker.Generate();
            var applicant = applicantFaker.Generate();
            applicant.UserId = user.Id;
            var otherUser = userFaker.Generate();
            var otherApplicant = applicantFaker.Generate();
            otherApplicant.UserId = user.Id;
            var notAccessibleApplication = applicationFaker.GenerateValid(otherApplicant);
            await context.AddAsync(user);
            await context.AddAsync(applicant);
            await context.AddAsync(otherUser);
            await context.AddAsync(otherApplicant);
            await context.AddAsync(notAccessibleApplication);
            ApplicationNotFoundByApplicantAndApplicationIdException? result = null;

            await context.SaveChangesAsync();

            try
            {
                await repository.GetByApplicantAndApplicationIdAsync(applicant.Id, notAccessibleApplication.Id, CancellationToken.None);
            }
            catch (Exception e)
            {
                result = e as ApplicationNotFoundByApplicantAndApplicationIdException;
            }

            result.Should().NotBeNull();
        }

        /// <summary>
        /// Test for <see cref="IVisaApplicationsRepository.GetByApplicantAndApplicationIdAsync"/> method
        /// that should return application for valid identifiers
        /// </summary>
        [Fact]
        private async Task GetApplicantIdByUserIdForValidIdsShouldReturnApplication()
        {
            await using var context = InMemoryContextProvider.GetDbContext();
            var repository = GetRepository(context);
            var user = userFaker.Generate();
            var applicant = applicantFaker.Generate();
            applicant.UserId = user.Id;
            var application = applicationFaker.GenerateValid(applicant);
            await context.AddAsync(user);
            await context.AddAsync(applicant);
            await context.AddAsync(application);

            await context.SaveChangesAsync();

            var result = await repository.GetByApplicantAndApplicationIdAsync(applicant.Id, application.Id, CancellationToken.None);

            result.Should().Be(application);
        }

        /// <summary>
        /// Test for <see cref="IVisaApplicationsRepository.GetPendingApplicationsAsync"/> method that should return empty from empty db
        /// </summary>
        [Fact]
        private async Task GetPendingApplicationsForEmptyShouldReturnEmpty()
        {
            await using var context = InMemoryContextProvider.GetDbContext();
            var repository = GetRepository(context);

            var result = await repository.GetPendingApplicationsAsync(CancellationToken.None);

            result.Should().BeEmpty();
        }

        /// <summary>
        /// Test for <see cref="IVisaApplicationsRepository.GetPendingApplicationsAsync"/> method that should return pending applications from not empty db
        /// </summary>
        [Fact]
        private async Task GetPendingApplicationsForExistingShouldReturnExistingPending()
        {
            await using var context = InMemoryContextProvider.GetDbContext();
            var repository = GetRepository(context);
            var user = userFaker.Generate();
            var applicant = applicantFaker.Generate();
            applicant.UserId = user.Id;
            var applicationPending = applicationFaker.GenerateValid(applicant);
            applicationPending.Status = ApplicationStatus.Pending;
            var applicationNotPending = applicationFaker.GenerateValid(applicant);
            applicationNotPending.Status = ApplicationStatus.Approved;
            await context.AddAsync(user);
            await context.AddAsync(applicant);
            await context.AddAsync(applicationPending);
            await context.AddAsync(applicationNotPending);

            await context.SaveChangesAsync();

            var result = await repository.GetPendingApplicationsAsync(CancellationToken.None);

            result.Should().Contain(applicationPending).And.HaveCount(1);
        }
    }
}
