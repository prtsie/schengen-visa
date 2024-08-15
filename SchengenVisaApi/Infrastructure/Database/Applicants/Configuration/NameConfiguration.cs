using Domains;
using Domains.ApplicantDomain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Applicants.Configuration;

public static class NameConfiguration<T> where T : class, IEntity
{
    public static void Configure(OwnedNavigationBuilder<T, Name> entity)
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
