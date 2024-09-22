using ApplicationLayer.InfrastructureServicesInterfaces;
using ApplicationLayer.Services.VisaApplications.Models;
using Bogus;

namespace VisaApi.Fakers.VisaApplications.Requests;

/// <summary>
/// Generates past visas
/// </summary>
public sealed class PastVisaModelFaker : Faker<PastVisaModel>
{
    private IDateTimeProvider dateTimeProvider;

    public PastVisaModelFaker(IDateTimeProvider dateTimeProvider)
    {
            this.dateTimeProvider = dateTimeProvider;

            RuleFor(pv => pv.Name, f => f.Random.Words());
        }

    public PastVisaModel GenerateValid()
    {
            var result = Generate();
            result.IssueDate = dateTimeProvider.Now().AddDays(-Random.Shared.Next(11, 900));
            result.ExpirationDate = result.IssueDate.AddDays(Random.Shared.Next(1, 11));
            return result;
        }
}
