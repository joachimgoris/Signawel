using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Signawel.Domain;

namespace Signawel.Data.Configurations
{
    internal class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("refresh_tokens");

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.ExpiryDate).HasColumnName("expiry_date");
            builder.Property(e => e.Token).HasColumnName("token");
            builder.Property(e => e.JwtId).HasColumnName("jwt_id");
            builder.Property(e => e.UserId).HasColumnName("user_id");

            builder.HasOne(e => e.User);
        }
    }
}
