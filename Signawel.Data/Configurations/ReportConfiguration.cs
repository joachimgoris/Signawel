using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Signawel.Domain.Reports;

namespace Signawel.Data.Configurations
{
    internal class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.ToTable("reports");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.SenderEmail)
                .IsUnicode()
                .HasColumnName("sender_email")
                .IsRequired();

            builder.Property(e => e.Description)
                .IsUnicode()
                .HasColumnName("description");

            builder.Property(e => e.RoadworkId)
               .IsUnicode()
               .IsRequired()
               .HasColumnName("roadwork_id");

            builder.Property(e => e.Description)
                .IsUnicode()
                .HasColumnName("description");

            builder.Property(e => e.Cities)
                .HasColumnName("cities");

            builder.Property(e => e.IssueId)
                .HasColumnName("issue_id");

            builder.Property(e => e.CreationTime)
                .HasColumnName("creation_time");
        }
    }
}
