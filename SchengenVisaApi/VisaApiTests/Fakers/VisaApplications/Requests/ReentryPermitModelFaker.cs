using ApplicationLayer.InfrastructureServicesInterfaces;
using ApplicationLayer.Services.VisaApplications.Models;
using Bogus;
using Domains;

namespace VisaApi.Fakers.VisaApplications.Requests;

/// <summary>
/// Generates re-entry permissions
/// </summary>
public sealed class ReentryPermitModelFaker : Faker<ReentryPermitModel>
{
    public ReentryPermitModelFaker(IDateTimeProvider dateTimeProvider)
    {
            RuleFor(p => p.Number,
                f => f.Random.String(ConfigurationConstraints.ReentryPermitNumberLength, 'a', 'z'));

            RuleFor(p => p.ExpirationDate,
                f => f.Date.Future(4, dateTimeProvider.Now()));
        }
}
