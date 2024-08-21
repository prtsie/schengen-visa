using Domains;
using Domains.VisaApplicationDomain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.VisaApplications.Configuration
{
    public static class PastVisitConfiguration<T> where T : class, IEntity
    {
        public static void Configure(OwnedNavigationBuilder<T, PastVisit> entity)
        {
            entity.Property(pv => pv.DestinationCountry)
                .IsUnicode(false)
                .HasMaxLength(ConfigurationConstraints.CountryNameLength);
        }
    }
}
