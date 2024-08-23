using ApplicationLayer.GeneralExceptions;

namespace ApplicationLayer.Services.VisaApplications.Exceptions
{
    public class ApplicationAlreadyProcessedException() : ApiException("This application already processed or closed by applicant.");
}
