using ApplicationLayer.InfrastructureServicesInterfaces;
using Bogus;
using Domains;
using Domains.ApplicantDomain;
using Domains.VisaApplicationDomain;

namespace VisaApi.Fakers.VisaApplications;

/// <summary>
/// Generates visa applications
/// </summary>
public sealed class VisaApplicationFaker : Faker<VisaApplication>
{
    private static ReentryPermitFaker reentryPermitFaker = null!;
    private static PermissionToDestCountryFaker permissionToDestCountryFaker = null!;

    public VisaApplicationFaker(IDateTimeProvider dateTimeProvider)
    {
            reentryPermitFaker = new(dateTimeProvider);
            permissionToDestCountryFaker = new(dateTimeProvider);
            var pastVisaFaker = new PastVisaFaker(dateTimeProvider);
            var pastVisitFaker = new PastVisitFaker(dateTimeProvider);

            RuleFor(va => va.Status, f => f.Random.Enum<ApplicationStatus>());

            RuleFor(va => va.DestinationCountry, f => f.Address.Country());

            RuleFor(va => va.PastVisas,
                f => f.PickRandom(pastVisaFaker.Generate(3), f.Random.Int(0, 3)).ToList());

            RuleFor(va => va.PastVisits,
                f => f.PickRandom(pastVisitFaker.Generate(3), f.Random.Int(0, 3)).ToList());

            RuleFor(va => va.VisaCategory, f => f.Random.Enum<VisaCategory>());

            RuleFor(va => va.ForGroup, f => f.Random.Bool());

            RuleFor(va => va.RequestedNumberOfEntries,
                f => f.Random.Enum<RequestedNumberOfEntries>());

            RuleFor(va => va.RequestDate, dateTimeProvider.Now);

            RuleFor(va => va.ValidDaysRequested,
                f => f.Random.Int(1, ConfigurationConstraints.MaxValidDays));
        }

    public VisaApplication GenerateValid(Applicant applicant)
    {
            var result = Generate();

            result.ApplicantId = applicant.Id;
            if (applicant.IsNonResident)
            {
                result.ReentryPermit = reentryPermitFaker.Generate();
            }

            if (result.VisaCategory is VisaCategory.Transit)
            {
                result.PermissionToDestCountry = permissionToDestCountryFaker.Generate();
            }

            return result;
        }
}