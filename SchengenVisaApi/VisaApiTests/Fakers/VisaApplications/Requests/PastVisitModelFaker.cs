using ApplicationLayer.InfrastructureServicesInterfaces;
using ApplicationLayer.Services.VisaApplications.Models;
using Bogus;

namespace VisaApi.Fakers.VisaApplications.Requests;

/// <summary>
/// Generates past visas
/// </summary>
public sealed class PastVisitModelFaker : Faker<PastVisitModel>
{
    private IDateTimeProvider dateTimeProvider;

    public PastVisitModelFaker(IDateTimeProvider dateTimeProvider)
    {
            this.dateTimeProvider = dateTimeProvider;

            RuleFor(pv => pv.DestinationCountry, f => f.Address.Country());
        }

    public PastVisitModel GenerateValid()
    {
            var result = Generate();
            result.StartDate = dateTimeProvider.Now().AddDays(-Random.Shared.Next(11, 900));
            result.EndDate = result.StartDate.AddDays(Random.Shared.Next(1, 11));
            return result;
        }
}
