using Domains.ApplicantDomain;
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
        entity.OwnsMany(va => va.PastVisits, PastVisitConfiguration<VisaApplication>.Configure);
        entity.OwnsMany(va => va.PastVisas, PastVisaConfiguration<VisaApplication>.Configure);

        entity.Property(va => va.DestinationCountry)
            .IsUnicode(false)
            .HasMaxLength(ConfigurationConstraints.CountryNameLength);

        entity.HasOne<Applicant>()
            .WithMany()
            .HasForeignKey(va => va.ApplicantId)
            .IsRequired();
    }
}
