using ApplicationLayer.InfrastructureServicesInterfaces;
using Bogus;
using Domains;
using Domains.ApplicantDomain;

namespace VisaApi.Fakers.Applicants
{
    public sealed class ApplicantFaker : Faker<Applicant>
    {
        public ApplicantFaker(IDateTimeProvider dateTimeProvider)
        {
            RuleFor(a => a.Citizenship, f => f.Address.Country());

            RuleFor(a => a.Gender, f => f.Random.Enum<Gender>());

            RuleForType(typeof(Name), f
                => new Name
                {
                    FirstName = f.Name.LastName(),
                    Surname = f.Name.LastName(),
                    Patronymic = f.Name.FirstName()
                });

            RuleFor(a => a.BirthDate,
                f => f.Date.Past(60, dateTimeProvider.Now()));

            RuleFor(a => a.Passport, f
                => new Passport
                {
                    Issuer = f.Company.CompanyName(),
                    Number = f.Random.String(ConfigurationConstraints.PasswordLength),
                    ExpirationDate = f.Date.Future(4, dateTimeProvider.Now()),
                    IssueDate = f.Date.Past(4, dateTimeProvider.Now())
                });

            RuleFor(a => a.JobTitle, f => f.Name.JobTitle());

            RuleFor(a => a.MaritalStatus, f => f.Random.Enum<MaritalStatus>());

            RuleFor(a => a.CitizenshipByBirth, f => f.Address.Country());

            RuleFor(a => a.CityOfBirth, f => f.Address.City());

            RuleFor(a => a.CountryOfBirth, f => f.Address.Country());

            RuleFor(a => a.IsNonResident, f => f.Random.Bool());

            RuleFor(a => a.PlaceOfWork, f
                => new PlaceOfWork
                {
                    Address = new Address
                    {
                        Country = f.Address.Country(),
                        City = f.Address.City(),
                        Street = f.Address.StreetName(),
                        Building = f.Address.BuildingNumber()
                    },
                    Name = f.Company.CompanyName(),
                    PhoneNum = f.Phone.PhoneNumber()
                });
        }
    }
}
