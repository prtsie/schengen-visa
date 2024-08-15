using ApplicationLayer.Locations.NeededServices;
using ApplicationLayer.VisaApplications.Models;
using ApplicationLayer.VisaApplications.NeededServices;
using ApplicationLayer.VisaApplications.Requests;
using Domains.ApplicantDomain;
using Domains.VisaApplicationDomain;

namespace ApplicationLayer.VisaApplications.Handlers;

/// Handles visa requests
public class VisaApplicationRequestsHandler(
    IVisaApplicationsRepository applications,
    ICitiesRepository cities,
    ICountriesRepository countries) : IVisaApplicationsRequestHandler
{
    public async Task<List<VisaApplication>> Get(CancellationToken cancellationToken) => await applications.GetAllAsync(cancellationToken);

    public async void HandleCreateRequest(VisaApplicationCreateRequest request, CancellationToken cancellationToken)
    {
        //TODO mapper
        var cityOfBirth = await cities.GetByIdAsync(request.BirthCityId, cancellationToken);
        var cityOfWork = await cities.GetByIdAsync(request.PlaceOfWork.Address.CityId, cancellationToken);

        var addressOfWork = new Address
        {
            City = cityOfWork,
            Country = cityOfWork.Country,
            Building = request.PlaceOfWork.Address.Building,
            Street = request.PlaceOfWork.Address.Street
        };

        var applicant = new Applicant
        {
            MaritalStatus = request.MaritalStatus,
            Name = request.FullName,
            Passport = request.Passport,
            Gender = request.Gender,
            Citizenship = request.CitizenShip,
            BirthDate = request.BirthDate,
            FatherName = request.FatherName,
            JobTitle = request.JobTitle,
            MotherName = request.MotherName,
            CitizenshipByBirth = request.CitizenshipByBirth,
            CityOfBirth = cityOfBirth,
            CountryOfBirth = cityOfBirth.Country,
            IsNonResident = request.IsNonResident,
            PlaceOfWork = new PlaceOfWork { Address = addressOfWork, Name = request.PlaceOfWork.Name, PhoneNum = request.PlaceOfWork.PhoneNum }
        };

        var pastVisits = request.PastVisits.Select(m => ConvertPastVisitModelToPastVisit(m, cancellationToken).Result).ToList();
        var visaApplication = new VisaApplication
        {
            Applicant = applicant,
            RequestedNumberOfEntries = request.RequestedNumberOfEntries,
            ValidDaysRequested = request.ValidDaysRequested,
            ReentryPermit = request.ReentryPermit,
            VisaCategory = request.VisaCategory,
            PermissionToDestCountry = request.PermissionToDestCountry,
            DestinationCountry = await countries.GetByIdAsync(request.DestinationCountryId, cancellationToken),
            PastVisas = request.PastVisas.ToList(),
            PastVisits = pastVisits,
            ForGroup = request.IsForGroup,
            RequestDate = DateTime.Today
        };

        await applications.AddAsync(visaApplication, cancellationToken);
    }

    private async Task<PastVisit> ConvertPastVisitModelToPastVisit(PastVisitModel model, CancellationToken cancellationToken)
    {
        return new PastVisit
        {
            DestinationCountry = await countries.GetByIdAsync(model.DestinationCountryId, cancellationToken),
            StartDate = model.StartDate,
            EndDate = model.EndDate
        };
    }
}
