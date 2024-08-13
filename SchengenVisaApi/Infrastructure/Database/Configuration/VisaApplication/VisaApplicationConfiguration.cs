using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration.VisaApplication
{
    public class VisaApplicationConfiguration : IEntityTypeConfiguration<Domains.VisaApplicationDomain.VisaApplication>
    {
        public void Configure(EntityTypeBuilder<Domains.VisaApplicationDomain.VisaApplication> entity)
        {
            entity.ToTable("VisaApplications");

            entity.OwnsOne(p => p.ReentryPermit);
            entity.OwnsOne(p => p.PermissionToDestCountry);
        }
    }
}
