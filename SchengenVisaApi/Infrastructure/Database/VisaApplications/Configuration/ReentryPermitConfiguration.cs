using Domains.VisaApplicationDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.VisaApplications.Configuration
{
    public class ReentryPermitConfiguration : IEntityTypeConfiguration<ReentryPermit>
    {
        public void Configure(EntityTypeBuilder<ReentryPermit> entity)
        {
            entity.Property(p => p.Number)
                .IsUnicode(false)
                .HasMaxLength(25);
        }
    }
}
