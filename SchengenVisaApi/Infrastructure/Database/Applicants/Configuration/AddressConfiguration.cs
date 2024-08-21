using Domains;
using Domains.ApplicantDomain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Applicants.Configuration;

public static class AddressConfiguration<T> where T : class, IEntity
{
    public static void Configure(OwnedNavigationBuilder<T, Address> entity)
    {
        entity.Property(a => a.Country)
            .IsUnicode(false)
            .HasMaxLength(ConfigurationConstraints.CountryNameLength);

        entity.Property(a => a.City)
            .IsUnicode(false)
            .HasMaxLength(ConfigurationConstraints.CityNameLength);

        entity.Property(a => a.Street)
            .IsUnicode(false)
            .HasMaxLength(ConfigurationConstraints.StreetNameLength);

        entity.Property(a => a.Building)
            .IsUnicode(false)
            .HasMaxLength(ConfigurationConstraints.BuildingNumberLength);
    }
}
