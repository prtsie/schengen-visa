using ApplicationLayer.InfrastructureServicesInterfaces;
using ApplicationLayer.Services.Applicants.NeededServices;
using FluentAssertions;
using Infrastructure.Database;
using Infrastructure.Database.Applicants.Repositories;
using Infrastructure.Database.Applicants.Repositories.Exceptions;
using VisaApi.Fakers;
using VisaApi.Services;

namespace VisaApi.Database.Repositories
{
    public class ApplicantsRepositoryTests
    {
        private static IApplicantsRepository GetRepository(DbContext context)
            => new ApplicantsRepository(context, context);

        private static IDateTimeProvider GetDateTimeProvider() => new TestDateTimeProvider();

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

        [Fact]
        private async Task FindByUserIdForExistingShouldReturnApplicant()
        {
            await using var context = InMemoryContextProvider.GetDbContext();
            var repository = GetRepository(context);
            var user = new UserFaker().Generate();
            var applicant = new ApplicantFaker(GetDateTimeProvider()).Generate();
            applicant.UserId = user.Id;
            await context.AddAsync(user);
            await repository.AddAsync(applicant, CancellationToken.None);
            await context.SaveChangesAsync();

            var result = await repository.FindByUserIdAsync(user.Id, CancellationToken.None);

            result.Should().BeEquivalentTo(applicant);
        }
    }
}
