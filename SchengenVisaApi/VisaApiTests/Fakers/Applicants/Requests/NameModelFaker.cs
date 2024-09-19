using ApplicationLayer.Services.Applicants.Models;
using Bogus;

namespace VisaApi.Fakers.Applicants.Requests
{
    public sealed class NameModelFaker : Faker<NameModel>
    {
        public NameModelFaker()
        {
            RuleFor(m => m.FirstName, f => f.Name.FirstName());

            RuleFor(m => m.Surname, f => f.Name.LastName());

            RuleFor(m => m.Patronymic, f => f.Name.FirstName());
        }
    }
}
