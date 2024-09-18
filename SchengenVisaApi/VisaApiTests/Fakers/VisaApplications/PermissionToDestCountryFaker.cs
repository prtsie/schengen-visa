using ApplicationLayer.InfrastructureServicesInterfaces;
using Bogus;
using Domains.VisaApplicationDomain;

namespace VisaApi.Fakers.VisaApplications
{
    /// <summary>
    /// Generates permissions to destination Country
    /// </summary>
    public sealed class PermissionToDestCountryFaker : Faker<PermissionToDestCountry>
    {
        public PermissionToDestCountryFaker(IDateTimeProvider dateTimeProvider)
        {
            RuleFor(p => p.Issuer, f => f.Company.CompanyName());

            RuleFor(p => p.ExpirationDate,
                f => f.Date.Future(4, dateTimeProvider.Now()));
        }
    }
}
