using Domains.VisaApplicationDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.VisaApplications.Configuration
{
    public class VisaApplicationConfiguration : IEntityTypeConfiguration<VisaApplication>
    {
        public void Configure(EntityTypeBuilder<VisaApplication> entity)
        {
            entity.ToTable("VisaApplications");

            entity.HasOne(va => va.Applicant)
                .WithMany(a => a.VisaApplications)
                .HasForeignKey(va => va.ApplicantId)
                .IsRequired();

            entity.OwnsOne(p => p.ReentryPermit);
            entity.OwnsOne(p => p.PermissionToDestCountry);
        }
    }
}
