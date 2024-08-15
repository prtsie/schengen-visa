using Domains;
using Domains.VisaApplicationDomain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.VisaApplications.Configuration;

public static class ReentryPermitConfiguration<T> where T : class, IEntity
{
    public static void Configure(OwnedNavigationBuilder<T, ReentryPermit> entity)
    {
        entity.Property(p => p.Number)
            .IsUnicode(false)
            .HasMaxLength(25);
    }
}
