using ApplicationLayer.GeneralNeededServices;
using ApplicationLayer.Services.Applicants.NeededServices;
using ApplicationLayer.Services.AuthServices.NeededServices;
using ApplicationLayer.Services.AuthServices.RegisterService.Exceptions;
using ApplicationLayer.Services.AuthServices.Requests;
using ApplicationLayer.Services.Locations.NeededServices;
using Domains.ApplicantDomain;
using Domains.Users;

namespace ApplicationLayer.Services.AuthServices.RegisterService
{
    /// <inheritdoc cref="IRegisterService"/>
    public class RegisterService(
        IUsersRepository users,
        IApplicantsRepository applicants,
        ICitiesRepository cities,
        IUnitOfWork unitOfWork) : IRegisterService
    {
        async Task IRegisterService.Register(RegisterApplicantRequest request, CancellationToken cancellationToken)
        {
            if (await users.FindByEmailAsync(request.Email, cancellationToken) is not null)
            {
                throw new UserAlreadyExistsException(request);
            }

            //TODO mapper
            var user = new User { Email = request.Email, Password = request.Password, Role = Role.Applicant };

            var applicantCity = await cities.GetByIdAsync(request.CityOfBirthId, cancellationToken);
            var placeOfWorkCity = await cities.GetByIdAsync(request.PlaceOfWork.Address.CityId, cancellationToken);
            var placeOfWorkAddress = new Address
            {
                Country = placeOfWorkCity.Country,
                City = placeOfWorkCity,
                Building = request.PlaceOfWork.Address.Building,
                Street = request.PlaceOfWork.Address.Street
            };

            var placeOfWork = new PlaceOfWork
            {
                Name = request.PlaceOfWork.Name,
                Address = placeOfWorkAddress,
                PhoneNum = request.PlaceOfWork.PhoneNum
            };

            var applicant = new Applicant
            {
                Citizenship = request.Citizenship,
                CitizenshipByBirth = request.CitizenshipByBirth,
                Gender = request.Gender,
                Name = request.ApplicantName,
                Passport = request.Passport,
                BirthDate = request.BirthDate,
                FatherName = request.FatherName,
                JobTitle = request.JobTitle,
                MaritalStatus = request.MaritalStatus,
                MotherName = request.MotherName,
                UserId = user.Id,
                CityOfBirth = applicantCity,
                CountryOfBirth = applicantCity.Country,
                IsNonResident = request.IsNonResident,
                PlaceOfWork = placeOfWork
            };

            await users.AddAsync(user, cancellationToken);
            await applicants.AddAsync(applicant, cancellationToken);

            await unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
