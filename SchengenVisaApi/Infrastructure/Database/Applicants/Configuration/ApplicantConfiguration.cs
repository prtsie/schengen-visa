using Domains;
using Domains.ApplicantDomain;
using Domains.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Applicants.Configuration;

public class ApplicantConfiguration : IEntityTypeConfiguration<Applicant>
{
    public void Configure(EntityTypeBuilder<Applicant> entity)
    {
        entity.OwnsOne(a => a.Name, NameConfiguration<Applicant>.Configure);
        entity.OwnsOne(a => a.FatherName, NameConfiguration<Applicant>.Configure);
        entity.OwnsOne(a => a.MotherName, NameConfiguration<Applicant>.Configure);
        entity.OwnsOne(a => a.Passport, PassportConfiguration<Applicant>.Configure);

        entity.HasOne<User>().WithOne().HasForeignKey<Applicant>(a => a.UserId);

        entity.Property(a => a.Citizenship)
            .IsUnicode(false)
            .HasMaxLength(ConfigurationConstraints.CitizenshipLength);

        entity.Property(a => a.CitizenshipByBirth)
            .IsUnicode(false)
            .HasMaxLength(ConfigurationConstraints.CitizenshipLength);

        entity.Property(a => a.CountryOfBirth)
            .IsUnicode(false)
            .HasMaxLength(ConfigurationConstraints.CountryNameLength);

        entity.Property(a => a.CityOfBirth)
            .IsUnicode(false)
            .HasMaxLength(ConfigurationConstraints.CityNameLength);

        entity.Property(a => a.JobTitle)
            .IsUnicode(false)
            .HasMaxLength(ConfigurationConstraints.JobTitleLength);
    }
}
