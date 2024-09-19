using ApplicationLayer.Services.Users.Requests;
using Bogus;
using VisaApi.Fakers.Common;

namespace VisaApi.Fakers.Users.Requests
{
    public sealed class ChangeUserAuthDataRequestFaker : Faker<ChangeUserAuthDataRequest>
    {
        private static ChangeAuthDataFaker changeAuthDataFaker = new();

        public ChangeUserAuthDataRequestFaker()
        {
            CustomInstantiator(_ => new(Guid.NewGuid(), changeAuthDataFaker.Generate()));
        }
    }
}
