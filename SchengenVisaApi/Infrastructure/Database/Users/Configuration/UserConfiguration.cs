using Domains;
using Domains.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Users.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.Property(u => u.Email)
                .IsUnicode(false)
                .HasMaxLength(ConfigurationConstraints.EmailLength);

            entity.HasIndex(u => u.Email).IsUnique();

            entity.Property(u => u.Password)
                .IsUnicode(false)
                .HasMaxLength(ConfigurationConstraints.PasswordLength);
        }
    }
}
