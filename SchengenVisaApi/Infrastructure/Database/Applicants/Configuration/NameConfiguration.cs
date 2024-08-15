using Domains.ApplicantDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Applicants.Configuration;

public class NameConfiguration : IEntityTypeConfiguration<Name>
{
    public void Configure(EntityTypeBuilder<Name> entity)
    {
        entity.Property(p => p.FirstName)
            .IsUnicode(false)
            .HasMaxLength(50);

        entity.Property(p => p.Surname)
            .IsUnicode(false)
            .HasMaxLength(50);

        entity.Property(p => p.Patronymic)
            .IsUnicode(false)
            .HasMaxLength(50);
    }
}