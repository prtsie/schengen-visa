using Domains.ApplicantDomain;

namespace ApplicationLayer.Services.Applicants.Models
{
    public class PlaceOfWorkModel
    {
        /// Name of hirer
        public string Name { get; set; } = null!;

        /// Address of hirer
        public Address Address { get; set; } = null!;

        /// Phone number of hirer
        public string PhoneNum { get; set; } = null!;
    }
}
