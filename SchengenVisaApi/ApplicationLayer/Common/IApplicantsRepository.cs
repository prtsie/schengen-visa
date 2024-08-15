using Domains.ApplicantDomain;

namespace ApplicationLayer.Common;

/// Repository pattern for <see cref="Applicant"/>
public interface IApplicantsRepository : IGenericRepository<Applicant> { }