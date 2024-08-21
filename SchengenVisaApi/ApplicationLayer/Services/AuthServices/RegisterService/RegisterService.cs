﻿using ApplicationLayer.InfrastructureServicesInterfaces;
using ApplicationLayer.Services.Applicants.NeededServices;
using ApplicationLayer.Services.AuthServices.NeededServices;
using ApplicationLayer.Services.AuthServices.RegisterService.Exceptions;
using ApplicationLayer.Services.AuthServices.Requests;
using Domains.ApplicantDomain;
using Domains.Users;

namespace ApplicationLayer.Services.AuthServices.RegisterService
{
    /// <inheritdoc cref="IRegisterService"/>
    public class RegisterService(
        IUsersRepository users,
        IApplicantsRepository applicants,
        IUnitOfWork unitOfWork) : IRegisterService
    {
        async Task IRegisterService.Register(RegisterApplicantRequest request, CancellationToken cancellationToken)
        {
            //todo move to validation layer
            if (await users.FindByEmailAsync(request.Email, cancellationToken) is not null)
            {
                throw new UserAlreadyExistsException(request);
            }

            //TODO mapper
            var user = new User { Email = request.Email, Password = request.Password, Role = Role.Applicant };

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
                CityOfBirth = request.CityOfBirth,
                CountryOfBirth = request.CountryOfBirth,
                IsNonResident = request.IsNonResident,
                PlaceOfWork = request.PlaceOfWork
            };

            await users.AddAsync(user, cancellationToken);
            await applicants.AddAsync(applicant, cancellationToken);

            await unitOfWork.SaveAsync(cancellationToken);
        }

        async Task IRegisterService.RegisterAuthority(RegisterRequest request, CancellationToken cancellationToken)
        {
            //todo move to validation layer
            if (await users.FindByEmailAsync(request.Email, cancellationToken) is not null)
            {
                throw new UserAlreadyExistsException(request);
            }

            //TODO mapper
            var user = new User { Email = request.Email, Password = request.Password, Role = Role.ApprovingAuthority };

            await users.AddAsync(user, cancellationToken);

            await unitOfWork.SaveAsync(cancellationToken);
        }
    }
}
