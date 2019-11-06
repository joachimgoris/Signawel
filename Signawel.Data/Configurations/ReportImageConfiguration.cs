using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Signawel.Domain;

namespace Signawel.Data.Configurations
{
    public class ReportImageConfiguration : IEntityTypeConfiguration<ReportImage>
    {
        public void Configure(EntityTypeBuilder<ReportImage> builder)
        {
            builder.ToTable("report_images");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.ImagePath)
                .IsRequired();

            builder.HasOne(e => e.Report)
                .WithMany(e => e.Images)
                .HasForeignKey(e => e.ReportId);
        }
    }
}
