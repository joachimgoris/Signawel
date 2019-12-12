using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Signawel.Domain;

namespace Signawel.Data.Configurations
{
    internal class BlacklistEmailConfiguration : IEntityTypeConfiguration<BlacklistEmail>
    {
        public void Configure(EntityTypeBuilder<BlacklistEmail> builder)
        {
            builder.ToTable("blacklist_emails");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.Email)
                .IsUnicode()
                .IsRequired()
                .HasColumnName("email");
        }
    }
}
