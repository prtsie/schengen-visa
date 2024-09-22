using ApplicationLayer.InfrastructureServicesInterfaces;
using Bogus;
using Domains;
using Domains.VisaApplicationDomain;

namespace VisaApi.Fakers.VisaApplications
{
    /// <summary>
    /// Generates re-entry permissions
    /// </summary>
    public sealed class ReentryPermitFaker : Faker<ReentryPermit>
    {
        public ReentryPermitFaker(IDateTimeProvider dateTimeProvider)
        {
            RuleFor(p => p.Number,
                f => f.Random.String(ConfigurationConstraints.ReentryPermitNumberLength, 'a', 'z'));

            RuleFor(p => p.ExpirationDate,
                f => f.Date.Future(4, dateTimeProvider.Now()));
        }
    }
}
