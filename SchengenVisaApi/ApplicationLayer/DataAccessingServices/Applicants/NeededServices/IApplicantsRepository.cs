using ApplicationLayer.GeneralNeededServices;
using Domains.ApplicantDomain;

namespace ApplicationLayer.DataAccessingServices.Applicants.NeededServices;

/// Repository pattern for <see cref="Applicant"/>
public interface IApplicantsRepository : IGenericRepository<Applicant>;
