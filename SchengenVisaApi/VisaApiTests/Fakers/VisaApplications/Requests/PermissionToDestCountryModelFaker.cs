using ApplicationLayer.InfrastructureServicesInterfaces;
using ApplicationLayer.Services.VisaApplications.Models;
using Bogus;

namespace VisaApi.Fakers.VisaApplications.Requests
{
    public sealed class PermissionToDestCountryModelFaker : Faker<PermissionToDestCountryModel>
    {
        public PermissionToDestCountryModelFaker(IDateTimeProvider dateTimeProvider)
        {
            RuleFor(p => p.Issuer, f => f.Company.CompanyName());

            RuleFor(p => p.ExpirationDate,
                f => f.Date.Future(4, dateTimeProvider.Now()));
        }
    }
}
