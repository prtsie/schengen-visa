using ApplicationLayer.InfrastructureServicesInterfaces;
using Bogus;
using Domains.VisaApplicationDomain;

namespace VisaApi.Fakers.VisaApplications;

/// <summary>
/// Generates past visas
/// </summary>
public sealed class PastVisitFaker : Faker<PastVisit>
{
    private IDateTimeProvider dateTimeProvider;

    public PastVisitFaker(IDateTimeProvider dateTimeProvider)
    {
            this.dateTimeProvider = dateTimeProvider;

            RuleFor(pv => pv.DestinationCountry, f => f.Address.Country());
        }

    public PastVisit GenerateValid()
    {
            var result = Generate();
            result.StartDate = dateTimeProvider.Now().AddDays(Random.Shared.Next(11, 900));
            result.EndDate = result.StartDate.AddDays(Random.Shared.Next(1, 11));
            return result;
        }
}