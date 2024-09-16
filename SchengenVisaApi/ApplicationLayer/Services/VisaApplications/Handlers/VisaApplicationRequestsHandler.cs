using ApplicationLayer.InfrastructureServicesInterfaces;
using ApplicationLayer.Services.Applicants.Models;
using ApplicationLayer.Services.Applicants.NeededServices;
using ApplicationLayer.Services.VisaApplications.Exceptions;
using ApplicationLayer.Services.VisaApplications.Models;
using ApplicationLayer.Services.VisaApplications.NeededServices;
using ApplicationLayer.Services.VisaApplications.Requests;
using AutoMapper;
using Domains.VisaApplicationDomain;

namespace ApplicationLayer.Services.VisaApplications.Handlers;

/// Handles visa requests
public class VisaApplicationRequestsHandler(
    IVisaApplicationsRepository applications,
    IApplicantsRepository applicants,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IDateTimeProvider dateTimeProvider,
    IUserIdProvider userIdProvider) : IVisaApplicationRequestsHandler
{
    async Task<List<VisaApplicationPreview>> IVisaApplicationRequestsHandler.GetPendingAsync(CancellationToken cancellationToken)
    {
        var applicationsList = await applications.GetPendingApplicationsAsync(cancellationToken);

        var applicationModels = mapper.Map<List<VisaApplicationPreview>>(applicationsList);
        return applicationModels;
    }

    async Task<List<VisaApplicationPreview>> IVisaApplicationRequestsHandler.GetForApplicantAsync(CancellationToken cancellationToken)
    {
        var applicantId = await applicants.GetApplicantIdByUserId(userIdProvider.GetUserId(), cancellationToken);
        var visaApplications = await applications.GetOfApplicantAsync(applicantId, cancellationToken);
        return mapper.Map<List<VisaApplicationPreview>>(visaApplications);
    }

    async Task<VisaApplicationModel> IVisaApplicationRequestsHandler.GetApplicationForApplicantAsync(Guid id, CancellationToken cancellationToken)
    {
        var applicant = await applicants.FindByUserIdAsync(userIdProvider.GetUserId(), cancellationToken);
        var application = await applications.GetByApplicantAndApplicationIdAsync(applicant.Id, id, cancellationToken);
        var mapped = mapper.Map<VisaApplicationModel>(application);
        mapped.Applicant = mapper.Map<ApplicantModel>(applicant);
        return mapped;
    }

    async Task<VisaApplicationModel> IVisaApplicationRequestsHandler.GetApplicationForAuthorityAsync(Guid id, CancellationToken cancellationToken)
    {
        var pending = await applications.GetPendingApplicationsAsync(cancellationToken);
        var application = pending.SingleOrDefault(a => a.Id == id) ?? throw new ApplicationAlreadyProcessedException();
        var mapped = mapper.Map<VisaApplicationModel>(application);
        var applicant = await applicants.GetByIdAsync(application.ApplicantId, cancellationToken);
        mapped.Applicant = mapper.Map<ApplicantModel>(applicant);
        return mapped;
    }

    async Task IVisaApplicationRequestsHandler.HandleCreateRequestAsync(VisaApplicationCreateRequest request, CancellationToken cancellationToken)
    {
        var applicant = await applicants.FindByUserIdAsync(userIdProvider.GetUserId(), cancellationToken);

        var visaApplication = mapper.Map<VisaApplication>(request);
        visaApplication.RequestDate = dateTimeProvider.Now();
        visaApplication.ApplicantId = applicant.Id;

        await applications.AddAsync(visaApplication, cancellationToken);

        await unitOfWork.SaveAsync(cancellationToken);
    }

    async Task IVisaApplicationRequestsHandler.HandleCloseRequestAsync(Guid applicationId, CancellationToken cancellationToken)
    {
        var applicantId = await applicants.GetApplicantIdByUserId(userIdProvider.GetUserId(), cancellationToken);
        var application = await applications.GetByApplicantAndApplicationIdAsync(applicantId, applicationId, cancellationToken);
        if (application.Status is ApplicationStatus.Approved or ApplicationStatus.Rejected)
        {
            throw new ApplicationAlreadyProcessedException();
        }

        application.Status = ApplicationStatus.Closed;
        await applications.UpdateAsync(application, cancellationToken);

        await unitOfWork.SaveAsync(cancellationToken);
    }

    async Task IVisaApplicationRequestsHandler.SetApplicationStatusFromAuthorityAsync(
        Guid applicationId,
        AuthorityRequestStatuses status,
        CancellationToken cancellationToken)
    {
        var application = await applications.GetByIdAsync(applicationId, cancellationToken);
        if (application.Status != ApplicationStatus.Pending)
        {
            throw new ApplicationAlreadyProcessedException();
        }

        var statusToSet = status switch
        {
            AuthorityRequestStatuses.Approved => ApplicationStatus.Approved,
            AuthorityRequestStatuses.Rejected => ApplicationStatus.Rejected,
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };

        application.Status = statusToSet;
        await applications.UpdateAsync(application, cancellationToken);

        await unitOfWork.SaveAsync(cancellationToken);
    }
}
