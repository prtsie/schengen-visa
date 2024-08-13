using Domains.ApplicantDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configuration.Applicant
{
    public class PlaceOfWorkConfiguration : IEntityTypeConfiguration<PlaceOfWork>
    {
        public void Configure(EntityTypeBuilder<PlaceOfWork> entity)
        {
            entity.OwnsOne(p => p.Address);

            entity.Property(p => p.Name)
                .IsUnicode(false)
                .HasMaxLength(200);

            entity.Property(p => p.PhoneNum)
                .IsUnicode(false)
                .HasMaxLength(20);
        }
    }
}
