using ApplicationLayer.Common;
using Domains.ApplicantDomain;

namespace ApplicationLayer.Applicants;

/// Repository pattern for <see cref="Applicant"/>
public interface IApplicantsRepository : IGenericRepository<Applicant> { }