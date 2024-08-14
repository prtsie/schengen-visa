using Domains.ApplicantDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Applicants.Configuration
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> entity)
        {
            entity.Property(p => p.Street)
                .IsUnicode(false)
                .HasMaxLength(100);
            entity.Property(p => p.Building)
                .IsUnicode(false)
                .HasMaxLength(10);
        }
    }
}
