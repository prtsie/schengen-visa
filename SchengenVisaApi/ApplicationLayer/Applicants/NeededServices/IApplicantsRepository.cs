using ApplicationLayer.GeneralNeededServices;
using Domains.ApplicantDomain;

namespace ApplicationLayer.Applicants.NeededServices;

/// Repository pattern for <see cref="Applicant"/>
public interface IApplicantsRepository : IGenericRepository<Applicant> { }
