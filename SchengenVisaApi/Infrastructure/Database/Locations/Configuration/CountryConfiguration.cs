using Domains.LocationDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Locations.Configuration;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> entity)
    {
        entity.Property(c => c.Name)
            .IsUnicode(false)
            .HasMaxLength(70);

        entity.HasIndex(c => c.Name).IsUnique();
    }
}
