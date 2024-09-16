using ApplicationLayer.GeneralExceptions;

namespace Infrastructure.Database.VisaApplications.Repositories.Exceptions;

public class ApplicationNotFoundByApplicantAndApplicationIdException(Guid applicationId)
    : EntityNotFoundException($"Application with id {applicationId} not found for authenticated user");