using Domains;
using Domains.VisaApplicationDomain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.VisaApplications.Configuration;

public static class PermissionToDestCountryConfiguration<T> where T : class, IEntity
{
    public static void Configure(OwnedNavigationBuilder<T, PermissionToDestCountry> entity)
    {
        entity.Property(p => p.Issuer)
            .IsUnicode(false)
            .HasMaxLength(ConfigurationConstraints.IssuerNameLength);
    }
}
