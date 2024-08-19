using ApplicationLayer.GeneralNeededServices;
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

    public async void HandleCreateRequest(Guid userId, VisaApplicationCreateRequest request, CancellationToken cancellationToken)
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
