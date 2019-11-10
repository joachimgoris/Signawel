using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Signawel.Domain;

namespace Signawel.Data.Configurations
{
    internal class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.ToTable("reports");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.UserEmail)
                .IsUnicode()
                .HasColumnName("user_email")
                .IsRequired();

            builder.Property(e => e.CustomMessage)
                .IsUnicode()
                .HasColumnName("custom_message");

            builder.Property(e => e.Priority)
                .IsRequired()
                .HasColumnName("priority");
        }
    }
}
