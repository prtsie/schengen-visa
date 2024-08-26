using ApplicationLayer.Services.GeneralExceptions;

namespace Infrastructure.Database.Applicants.Repositories.Exceptions;

public class ApplicantNotFoundByUserIdException() : EntityNotFoundException("Applicant not found.");
