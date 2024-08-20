using ApplicationLayer.InfrastructureServicesInterfaces;
using ApplicationLayer.Services.Applicants.NeededServices;
using ApplicationLayer.Services.Locations.NeededServices;
using ApplicationLayer.Services.VisaApplications.Models;
using ApplicationLayer.Services.VisaApplications.NeededServices;
using ApplicationLayer.Services.VisaApplications.Requests;
using Domains.VisaApplicationDomain;

namespace ApplicationLayer.Services.VisaApplications.Handlers;

/// Handles visa requests
public class VisaApplicationRequestsHandler(
    IVisaApplicationsRepository applications,
    IApplicantsRepository applicants,
    ICountriesRepository countries,
    IUnitOfWork unitOfWork) : IVisaApplicationRequestsHandler
{
    public async Task<List<VisaApplication>> Get(CancellationToken cancellationToken) => await applications.GetAllAsync(cancellationToken);

    public async Task<List<VisaApplicationModelForApplicant>> GetForApplicant(Guid userId, CancellationToken cancellationToken)
    {
        //todo mapper
        var applicantId = await applicants.GetApplicantIdByUserId(userId, cancellationToken);
        var visaApplications = await applications.GetOfApplicantAsync(applicantId, cancellationToken);
        return visaApplications.Select(va => new VisaApplicationModelForApplicant
            {
                DestinationCountry = va.DestinationCountry.Name,
                ValidDaysRequested = va.ValidDaysRequested,
                ReentryPermit = va.ReentryPermit,
                VisaCategory = va.VisaCategory,
                RequestedNumberOfEntries = va.RequestedNumberOfEntries,
                PermissionToDestCountry = va.PermissionToDestCountry,
                ForGroup = va.ForGroup,
                PastVisas = va.PastVisas,
                RequestDate = va.RequestDate,
                PastVisits = va.PastVisits.Select(pv =>
                    new PastVisitModel { DestinationCountry = pv.DestinationCountry.Name, StartDate = pv.StartDate, EndDate = pv.EndDate }).ToList()
            }).ToList();
    }

    public async Task HandleCreateRequest(Guid userId, VisaApplicationCreateRequest request, CancellationToken cancellationToken)
    {
        //TODO fix
        //TODO mapper

        var applicant = await applicants.FindByUserIdAsync(userId, cancellationToken);

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

        await unitOfWork.SaveAsync(cancellationToken);
    }

    private async Task<PastVisit> ConvertPastVisitModelToPastVisit(PastVisitModelForRequest modelForRequest, CancellationToken cancellationToken)
    {
        return new PastVisit
        {
            DestinationCountry = await countries.GetByIdAsync(modelForRequest.DestinationCountryId, cancellationToken),
            StartDate = modelForRequest.StartDate,
            EndDate = modelForRequest.EndDate
        };
    }
}
