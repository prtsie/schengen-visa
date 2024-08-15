using Domains.VisaApplicationDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.VisaApplications.Configuration;

public class PastVisaConfiguration : IEntityTypeConfiguration<PastVisa>
{
    public void Configure(EntityTypeBuilder<PastVisa> entity)
    {
        entity.Property(p => p.Name)
            .IsUnicode(false)
            .HasMaxLength(70);
    }
}