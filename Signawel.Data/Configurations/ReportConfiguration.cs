using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Signawel.Domain;

namespace Signawel.Data.Configurations
{
    public class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.ToTable("reports");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("report_id");

            builder.Property(e => e.UserEmail)
                .IsUnicode()
                .HasColumnName("user_email")
                .IsRequired();

            builder.Property(e => e.CustomMessage)
                .IsUnicode()
                .HasColumnName("custom_message");

            builder.Property(e => e.Priority)
                .IsRequired();
        }
    }
}
