using ApplicationLayer.Services.Applicants.Models;
using Bogus;

namespace VisaApi.Fakers.Applicants.Requests;

public sealed class PlaceOfWorkModelFaker : Faker<PlaceOfWorkModel>
{
    public PlaceOfWorkModelFaker()
    {
            RuleFor(m => m.Name, f => f.Company.CompanyName());

            RuleFor(m => m.PhoneNum, f => f.Phone.PhoneNumber("###########"));

            RuleFor(m => m.Address,
                f => new AddressModel
                {
                    Country = f.Address.Country(),
                    City = f.Address.City(),
                    Street = f.Address.StreetName(),
                    Building = f.Address.BuildingNumber()
                });
        }
}