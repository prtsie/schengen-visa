using ApplicationLayer.Services.AuthServices.Requests;
using Bogus;

namespace VisaApi.Fakers.Auth
{
    public sealed class RegisterRequestFaker : Faker<RegisterRequest>
    {
        private static AuthDataFaker authDataFaker = new();

        public RegisterRequestFaker()
        {
            RuleFor(r => r.AuthData, () => authDataFaker.Generate());
        }
    }
}
