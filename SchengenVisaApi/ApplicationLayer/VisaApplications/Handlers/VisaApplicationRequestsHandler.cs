using ApplicationLayer.Applicants;
using ApplicationLayer.VisaApplications.Requests;

namespace ApplicationLayer.VisaApplications.Handlers
{
    /// Handles visa requests
    public class VisaApplicationRequestsHandler(IApplicantsRepository)
    {
        public void HandleCreateRequest(CreateVisaApplicationRequest request)
        {

        }
    }
}
