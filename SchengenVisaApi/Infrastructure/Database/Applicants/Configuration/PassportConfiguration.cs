using Domains;
using Domains.ApplicantDomain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Applicants.Configuration;

public static class PassportConfiguration<T> where T : class, IEntity
{
    public static void Configure(OwnedNavigationBuilder<T, Passport> entity)
    {
        entity.Property(p => p.Number)
            .IsUnicode(false)
            .HasMaxLength(ConfigurationConstraints.PassportNumberLength);

        entity.Property(p => p.Issuer)
            .IsUnicode(false)
            .HasMaxLength(ConfigurationConstraints.IssuerNameLength);
    }
}
