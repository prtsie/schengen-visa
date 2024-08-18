using Domains.VisaApplicationDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.VisaApplications.Configuration;

public class VisaApplicationConfiguration : IEntityTypeConfiguration<VisaApplication>
{
    public void Configure(EntityTypeBuilder<VisaApplication> entity)
    {
        entity.OwnsOne(va => va.ReentryPermit, ReentryPermitConfiguration<VisaApplication>.Configure);
        entity.OwnsOne(va => va.PermissionToDestCountry, PermissionToDestCountryConfiguration<VisaApplication>.Configure);
        entity.OwnsMany(va => va.PastVisits, PastVisitConfiguration<VisaApplication>.Configure).ToTable("PastVisits");
        entity.OwnsMany(va => va.PastVisas).ToTable("PastVisas");

        entity.HasOne(va => va.DestinationCountry).WithMany().OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(va => va.Applicant)
            .WithMany()
            .HasForeignKey(va => va.ApplicantId)
            .IsRequired();
    }
}
