using Domains.VisaApplicationDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration.VisaApplication
{
    public class PermissionToDestCountryConfiguration : IEntityTypeConfiguration<PermissionToDestCountry>
    {
        public void Configure(EntityTypeBuilder<PermissionToDestCountry> entity)
        {
            entity.Property(p => p.Issuer)
                .IsUnicode(false)
                .HasMaxLength(200);
        }
    }
}
