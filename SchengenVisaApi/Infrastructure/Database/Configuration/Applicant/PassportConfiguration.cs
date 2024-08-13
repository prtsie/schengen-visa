using Domains.ApplicantDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration.Applicant
{
    public class PassportConfiguration : IEntityTypeConfiguration<Passport>
    {
        public void Configure(EntityTypeBuilder<Passport> entity)
        {
            entity.Property(p => p.Number)
                .IsUnicode(false)
                .HasMaxLength(20);

            entity.Property(p => p.Issuer)
                .IsUnicode(false)
                .HasMaxLength(200);
        }
    }
}
