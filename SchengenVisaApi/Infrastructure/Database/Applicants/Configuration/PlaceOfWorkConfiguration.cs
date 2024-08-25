using Domains;
using Domains.ApplicantDomain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Applicants.Configuration;

public class PlaceOfWorkConfiguration : IEntityTypeConfiguration<PlaceOfWork>
{
    public void Configure(EntityTypeBuilder<PlaceOfWork> entity)
    {
        entity.OwnsOne(p => p.Address, AddressConfiguration<PlaceOfWork>.Configure);

        entity.Property(p => p.Name)
            .IsUnicode(false)
            .HasMaxLength(ConfigurationConstraints.PlaceOfWorkNameLength);

        entity.Property(p => p.PhoneNum)
            .IsUnicode(false)
            .HasMaxLength(ConfigurationConstraints.PhoneNumberLength);
    }
}
