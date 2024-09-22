using ApplicationLayer.InfrastructureServicesInterfaces;
using ApplicationLayer.Services.Applicants.Models;
using Bogus;
using Domains;

namespace VisaApi.Fakers.Applicants.Requests
{
    public sealed class PassportModelFaker : Faker<PassportModel>
    {
        public PassportModelFaker(IDateTimeProvider dateTimeProvider)
        {
            RuleFor(m => m.Issuer, f => f.Company.CompanyName());

            RuleFor(m => m.Number,
                f => f.Random.String(ConfigurationConstraints.PassportNumberLength, 'a', 'z'));

            RuleFor(m => m.ExpirationDate,
                f => f.Date.Future(4, dateTimeProvider.Now()));

            RuleFor(m => m.IssueDate,
                f => f.Date.Past(4, dateTimeProvider.Now()));
        }
    }
}
