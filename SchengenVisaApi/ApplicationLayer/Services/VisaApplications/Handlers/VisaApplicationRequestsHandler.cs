using ApplicationLayer.InfrastructureServicesInterfaces;
using ApplicationLayer.Services.Applicants.Models;
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
    async Task<List<VisaApplicationModelForAuthority>> IVisaApplicationRequestsHandler.GetAllAsync(CancellationToken cancellationToken)
    {
        var applicationsList = await applications.GetAllAsync(cancellationToken);

        //todo mapper
        var applicationModels = applicationsList
            .Select(a => MapVisaApplicationToModelForAuthorities(a, cancellationToken).Result)
            .ToList();
        return applicationModels;
    }

    private async Task<VisaApplicationModelForAuthority> MapVisaApplicationToModelForAuthorities(VisaApplication visaApplication,
        CancellationToken cancellationToken)
    {
        var applicant = await applicants.GetByIdAsync(visaApplication.ApplicantId, cancellationToken);
        var applicantModel = new ApplicantModel
        {
            Citizenship = applicant.Citizenship,
            Gender = applicant.Gender,
            Name = applicant.Name,
            Passport = applicant.Passport,
            BirthDate = applicant.BirthDate,
            FatherName = applicant.FatherName,
            JobTitle = applicant.JobTitle,
            MaritalStatus = applicant.MaritalStatus,
            MotherName = applicant.MotherName,
            CitizenshipByBirth = applicant.CitizenshipByBirth,
            CityOfBirth = applicant.CityOfBirth,
            CountryOfBirth = applicant.CountryOfBirth,
            IsNonResident = applicant.IsNonResident,
            PlaceOfWork = applicant.PlaceOfWork,
        };
        return new VisaApplicationModelForAuthority
        {
            PastVisits = visaApplication.PastVisits,
            ReentryPermit = visaApplication.ReentryPermit,
            VisaCategory = visaApplication.VisaCategory,
            PermissionToDestCountry = visaApplication.PermissionToDestCountry,
            DestinationCountry = visaApplication.DestinationCountry,
            PastVisas = visaApplication.PastVisas,
            RequestDate = visaApplication.RequestDate,
            ValidDaysRequested = visaApplication.ValidDaysRequested,
            RequestedNumberOfEntries = visaApplication.RequestedNumberOfEntries,
            ForGroup = visaApplication.ForGroup,
            Applicant = applicantModel,
            Id = visaApplication.Id,
            Status = visaApplication.Status
        };
    }

    public async Task<List<VisaApplicationModelForApplicant>> GetForApplicantAsync(Guid userId, CancellationToken cancellationToken)
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
                PastVisits = va.PastVisits,
                Id = va.Id,
                Status = va.Status
            })
            .ToList();
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
            RequestDate = DateTime.Today,
            Status = ApplicationStatus.Pending
        };

        await applications.AddAsync(visaApplication, cancellationToken);

        await unitOfWork.SaveAsync(cancellationToken);
    }

    async Task IVisaApplicationRequestsHandler.HandleCloseRequest(Guid userId, Guid applicationId, CancellationToken cancellationToken)
    {
        var applicantId = await applicants.GetApplicantIdByUserId(userId, cancellationToken);
        var application = await applications.GetByApplicantAndApplicationIdAsync(applicantId, applicationId, cancellationToken);

        application.Status = ApplicationStatus.Closed;
        await applications.UpdateAsync(application, cancellationToken);

        await unitOfWork.SaveAsync(cancellationToken);
    }
}
