using ApplicationLayer.Services.GeneralExceptions;
using Domains.ApplicantDomain;

namespace Infrastructure.Database.Applicants.Repositories.Exceptions
{
    public class ApplicantNotFoundByUserIdException() : EntityNotFoundException("Applicant not found.");
}
