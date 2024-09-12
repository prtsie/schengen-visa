using ApplicationLayer.GeneralExceptions;

namespace Infrastructure.Database.Applicants.Repositories.Exceptions;

public class ApplicantNotFoundByUserIdException() : EntityNotFoundException("Applicant not found.");
