using Domains;
using Domains.VisaApplicationDomain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.VisaApplications.Configuration;

public static class PastVisaConfiguration<T> where T : class, IEntity
{
    public static void Configure(OwnedNavigationBuilder<T, PastVisa> entity)
    {
        entity.Property(p => p.Name)
            .IsUnicode(false)
            .HasMaxLength(ConfigurationConstraints.VisaNameLength);
    }
}
