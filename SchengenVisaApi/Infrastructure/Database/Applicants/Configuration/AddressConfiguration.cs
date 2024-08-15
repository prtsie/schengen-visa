using Domains;
using Domains.ApplicantDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Applicants.Configuration;

public static class AddressConfiguration<T> where T : class, IEntity
{
    public static void Configure(OwnedNavigationBuilder<T, Address> entity)
    {
        entity.HasOne(a => a.Country).WithMany().OnDelete(DeleteBehavior.Restrict);
        entity.HasOne(a => a.City).WithMany().OnDelete(DeleteBehavior.Restrict);

        entity.Property(p => p.Street)
            .IsUnicode(false)
            .HasMaxLength(100);

        entity.Property(p => p.Building)
            .IsUnicode(false)
            .HasMaxLength(10);
    }
}
