using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Signawel.Domain;

namespace Signawel.Data.Configurations
{
    internal class ReportImageConfiguration : IEntityTypeConfiguration<ReportImage>
    {
        public void Configure(EntityTypeBuilder<ReportImage> builder)
        {
            builder.ToTable("report_images");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.ImagePath)
                .IsRequired()
                .HasColumnName("image_path");
            builder.Property(e => e.ReportId)
                .HasColumnName("report_id");

            builder.HasOne(e => e.Report)
                .WithMany(e => e.Images)
                .HasForeignKey(e => e.ReportId);
        }
    }
}
