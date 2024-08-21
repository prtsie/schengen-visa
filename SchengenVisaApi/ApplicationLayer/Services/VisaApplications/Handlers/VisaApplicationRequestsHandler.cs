using ApplicationLayer.InfrastructureServicesInterfaces;
using ApplicationLayer.Services.Applicants.NeededServices;
using ApplicationLayer.Services.VisaApplications.Models;
using ApplicationLayer.Services.VisaApplications.NeededServices;
using ApplicationLayer.Services.VisaApplications.Requests;
using Domains.VisaApplicationDomain;

namespace ApplicationLayer.Services.VisaApplications.Handlers;

/// Handles visa requests
public class VisaApplicationRequestsHandler(
    IVisaApplicationsRepository applications,
    IApplicantsRepository applicants,
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
                DestinationCountry = va.DestinationCountry,
                ValidDaysRequested = va.ValidDaysRequested,
                ReentryPermit = va.ReentryPermit,
                VisaCategory = va.VisaCategory,
                RequestedNumberOfEntries = va.RequestedNumberOfEntries,
                PermissionToDestCountry = va.PermissionToDestCountry,
                ForGroup = va.ForGroup,
                PastVisas = va.PastVisas,
                RequestDate = va.RequestDate,
                PastVisits = va.PastVisits
            }).ToList();
    }

    public async Task HandleCreateRequest(Guid userId, VisaApplicationCreateRequest request, CancellationToken cancellationToken)
    {
        //TODO mapper

        var applicant = await applicants.FindByUserIdAsync(userId, cancellationToken);

        var visaApplication = new VisaApplication
        {
            ApplicantId = applicant.Id,
            RequestedNumberOfEntries = request.RequestedNumberOfEntries,
            ValidDaysRequested = request.ValidDaysRequested,
            ReentryPermit = request.ReentryPermit,
            VisaCategory = request.VisaCategory,
            PermissionToDestCountry = request.PermissionToDestCountry,
            DestinationCountry = request.DestinationCountry,
            PastVisas = request.PastVisas.ToList(),
            PastVisits = request.PastVisits.ToList(),
            ForGroup = request.IsForGroup,
            RequestDate = DateTime.Today
        };

        await applications.AddAsync(visaApplication, cancellationToken);

        await unitOfWork.SaveAsync(cancellationToken);
    }
}
