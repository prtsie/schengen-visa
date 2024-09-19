using ApplicationLayer.Services.AuthServices.Common;
using Bogus;

namespace VisaApi.Fakers.Auth
{
    public sealed class AuthDataFaker : Faker<AuthData>
    {
        public AuthDataFaker()
        {
            RuleFor(a => a.Email, f => f.Internet.Email());

            RuleFor(a => a.Password, f => f.Internet.Password());
        }
    }
}
