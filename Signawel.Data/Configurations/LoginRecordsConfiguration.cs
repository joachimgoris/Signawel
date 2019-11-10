
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Signawel.Domain;

namespace Signawel.Data.Configurations
{
    internal class LoginRecordsConfiguration : IEntityTypeConfiguration<LoginRecord>
    {
        public void Configure(EntityTypeBuilder<LoginRecord> builder)
        {
            builder.ToTable("login_records");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.IpAddress).HasColumnName("ip_address");
            builder.Property(e => e.Succes).HasColumnName("succes");
            builder.Property(e => e.UserId).HasColumnName("user_id");

            builder.HasOne(e => e.User);
        }
    }
}
