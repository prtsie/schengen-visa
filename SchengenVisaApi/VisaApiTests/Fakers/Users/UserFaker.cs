using Bogus;
using Domains.Users;

namespace VisaApi.Fakers.Users;

/// <summary>
/// Generates users
/// </summary>
public sealed class UserFaker : Faker<User>
{
    public UserFaker()
    {
            RuleFor(u => u.Email, f => f.Internet.Email());

            RuleFor(u => u.Password, f => f.Internet.Password());
        }
}