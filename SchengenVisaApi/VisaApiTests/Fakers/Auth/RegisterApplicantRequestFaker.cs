using ApplicationLayer.InfrastructureServicesInterfaces;
using ApplicationLayer.Services.AuthServices.Requests;
using Bogus;
using Domains.ApplicantDomain;
using VisaApi.Fakers.Applicants.Requests;

namespace VisaApi.Fakers.Auth
{
    public sealed class RegisterApplicantRequestFaker : Faker<RegisterApplicantRequest>
    {
        private readonly NameModelFaker nameModelFaker = new();
        private readonly PassportModelFaker passportModelFaker = new();
        private readonly PlaceOfWorkModelFaker placeOfWorkModelFaker = new();
        private readonly RegisterRequestFaker registerRequestFaker = new();

        public RegisterApplicantRequestFaker(IDateTimeProvider dateTimeProvider)
        {
            RuleFor(x => x.ApplicantName, () => nameModelFaker.Generate());
            RuleFor(x => x.BirthDate, faker => dateTimeProvider.Now().AddYears(-faker.Random.Int(20, 100)));
            RuleFor(x => x.Citizenship, faker => faker.Address.Country());
            RuleFor(x => x.CitizenshipByBirth, faker => faker.Address.Country());
            RuleFor(x => x.CityOfBirth, faker => faker.Address.City());
            RuleFor(x => x.CountryOfBirth, faker => faker.Address.Country());
            RuleFor(x => x.FatherName, () => nameModelFaker.Generate());
            RuleFor(x => x.Gender, faker => faker.PickRandom<Gender>());
            RuleFor(x => x.IsNonResident, faker => faker.Random.Bool());
            RuleFor(x => x.JobTitle, faker => faker.Lorem.Word());
            RuleFor(x => x.MaritalStatus, faker => faker.PickRandom<MaritalStatus>());
            RuleFor(x => x.MotherName, () => nameModelFaker.Generate());
            RuleFor(x => x.Passport, () => passportModelFaker.Generate());
            RuleFor(x => x.PlaceOfWork, () => placeOfWorkModelFaker.Generate());
            RuleFor(x => x.RegisterRequest, () => registerRequestFaker.Generate());
        }
    }
}
