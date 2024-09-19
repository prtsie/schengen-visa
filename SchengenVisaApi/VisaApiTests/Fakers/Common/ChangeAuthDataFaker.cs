using ApplicationLayer.Services.Users.Models;
using Bogus;

namespace VisaApi.Fakers.Common
{
    public sealed class ChangeAuthDataFaker : Faker<ChangeAuthData>
    {
        public ChangeAuthDataFaker()
        {
            RuleFor(a => a.Email, f => f.Internet.Email());

            RuleFor(a => a.Password, f => f.Internet.Password());
        }
    }
}
