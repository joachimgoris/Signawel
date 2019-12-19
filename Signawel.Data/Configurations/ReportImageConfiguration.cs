using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Signawel.Domain;
using Signawel.Domain.Reports;

namespace Signawel.Data.Configurations
{
    internal class ReportImageConfiguration : IEntityTypeConfiguration<ReportImage>
    {
        public void Configure(EntityTypeBuilder<ReportImage> builder)
        {
            builder.ToTable("report_images");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.ImageId)
                .IsRequired()
                .HasColumnName("image_id");

            builder.Property(e => e.ReportId)
                .HasColumnName("report_id");

            builder.HasOne(e => e.Report)
                .WithMany(e => e.Images)
                .HasForeignKey(e => e.ReportId);
        }
    }
}
