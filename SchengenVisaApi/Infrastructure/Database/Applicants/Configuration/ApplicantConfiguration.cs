using Domains.ApplicantDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Applicants.Configuration;

public class ApplicantConfiguration : IEntityTypeConfiguration<Applicant>
{
    public void Configure(EntityTypeBuilder<Applicant> entity)
    {
        entity.ToTable("Applicants");
        entity.OwnsOne(p => p.Name, NameConfiguration<Applicant>.Configure);
        entity.OwnsOne(p => p.FatherName, NameConfiguration<Applicant>.Configure);
        entity.OwnsOne(p => p.MotherName, NameConfiguration<Applicant>.Configure);
        entity.OwnsOne(p => p.Passport, PassportConfiguration<Applicant>.Configure);

        entity.HasOne(a => a.CityOfBirth).WithMany().OnDelete(DeleteBehavior.Restrict);
        entity.HasOne(a => a.CountryOfBirth).WithMany().OnDelete(DeleteBehavior.Restrict);

        entity.Property(p => p.Citizenship)
            .IsUnicode(false)
            .HasMaxLength(30);

        entity.Property(p => p.CitizenshipByBirth)
            .IsUnicode(false)
            .HasMaxLength(30);
    }
}
