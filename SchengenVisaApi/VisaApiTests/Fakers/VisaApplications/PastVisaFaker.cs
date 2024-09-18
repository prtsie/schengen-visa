using ApplicationLayer.InfrastructureServicesInterfaces;
using Bogus;
using Domains.VisaApplicationDomain;

namespace VisaApi.Fakers.VisaApplications
{
    /// <summary>
    /// Generates past visas
    /// </summary>
    public sealed class PastVisaFaker : Faker<PastVisa>
    {
        private IDateTimeProvider dateTimeProvider;

        public PastVisaFaker(IDateTimeProvider dateTimeProvider)
        {
            this.dateTimeProvider = dateTimeProvider;

            RuleFor(pv => pv.Name, f => f.Random.Words());
        }

        public PastVisa GenerateValid()
        {
            var result = Generate();
            result.IssueDate = dateTimeProvider.Now().AddDays(Random.Shared.Next(11, 900));
            result.ExpirationDate = result.IssueDate.AddDays(Random.Shared.Next(1, 11));
            return result;
        }
    }
}
