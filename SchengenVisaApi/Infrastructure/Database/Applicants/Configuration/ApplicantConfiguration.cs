using Domains.ApplicantDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Applicants.Configuration
{
    public class ApplicantConfiguration : IEntityTypeConfiguration<Applicant>
    {
        public void Configure(EntityTypeBuilder<Applicant> entity)
        {
            entity.ToTable("Applicants");

            entity.OwnsOne(p => p.Name);
            entity.OwnsOne(p => p.FatherName);
            entity.OwnsOne(p => p.MotherName);
            entity.OwnsOne(p => p.Passport);

            entity.Property(p => p.Citizenship)
                .IsUnicode(false)
                .HasMaxLength(30);

            entity.Property(p => p.CitizenshipByBirth)
                .IsUnicode(false)
                .HasMaxLength(30);
        }
    }
}
