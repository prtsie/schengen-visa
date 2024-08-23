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
    IDateTimeProvider dateTimeProvider) : IVisaApplicationRequestsHandler
{
    async Task<List<VisaApplicationModelForAuthority>> IVisaApplicationRequestsHandler.GetAllAsync(CancellationToken cancellationToken)
    {
        var applicationsList = await applications.GetAllAsync(cancellationToken);

        var applicationModels = applicationsList
            .Select(a => MapVisaApplicationToModelForAuthorities(a, cancellationToken).Result)
            .ToList();
        return applicationModels;
    }

    private async Task<VisaApplicationModelForAuthority> MapVisaApplicationToModelForAuthorities(VisaApplication visaApplication,
        CancellationToken cancellationToken)
    {
        var applicant = await applicants.GetByIdAsync(visaApplication.ApplicantId, cancellationToken);
        var applicantModel = mapper.Map<ApplicantModel>(applicant);

        var model = mapper.Map<VisaApplicationModelForAuthority>(visaApplication);
        model.Applicant = applicantModel;

        return model;
    }

    public async Task<List<VisaApplicationModelForApplicant>> GetForApplicantAsync(Guid userId, CancellationToken cancellationToken)
    {
        var applicantId = await applicants.GetApplicantIdByUserId(userId, cancellationToken);
        var visaApplications = await applications.GetOfApplicantAsync(applicantId, cancellationToken);
        return mapper.Map<List<VisaApplicationModelForApplicant>>(visaApplications);
    }

    public async Task HandleCreateRequestAsync(Guid userId, VisaApplicationCreateRequest request, CancellationToken cancellationToken)
    {
        var applicant = await applicants.FindByUserIdAsync(userId, cancellationToken);

        var visaApplication = mapper.Map<VisaApplication>(request);
        visaApplication.RequestDate = dateTimeProvider.Now();
        visaApplication.ApplicantId = applicant.Id;

        await applications.AddAsync(visaApplication, cancellationToken);

        await unitOfWork.SaveAsync(cancellationToken);
    }

    async Task IVisaApplicationRequestsHandler.HandleCloseRequestAsync(Guid userId, Guid applicationId, CancellationToken cancellationToken)
    {
        var applicantId = await applicants.GetApplicantIdByUserId(userId, cancellationToken);
        var application = await applications.GetByApplicantAndApplicationIdAsync(applicantId, applicationId, cancellationToken);

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
            //todo refactor exceptions
            throw new ApplicationAlreadyProcessedException();
        }

        //todo handle exception or not
        ApplicationStatus statusToSet = status switch
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
