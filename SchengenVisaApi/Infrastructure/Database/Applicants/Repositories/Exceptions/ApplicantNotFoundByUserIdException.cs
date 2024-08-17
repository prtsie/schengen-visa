﻿using Domains.ApplicantDomain;
using Infrastructure.Database.GeneralExceptions;

namespace Infrastructure.Database.Applicants.Repositories.Exceptions
{
    public class ApplicantNotFoundByUserIdException(Guid id) : EntityNotFoundException<Applicant>("Applicant not found.")
    {
        public Guid UserId { get; private set; } = id;
    }
}
