using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Signawel.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Signawel.Data.Configurations
{
    public class BBoxPointConfiguration : IEntityTypeConfiguration<BBoxPoint>
    {
        public void Configure(EntityTypeBuilder<BBoxPoint> builder)
        {
            builder.ToTable("bbox_points");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("point_id");

            builder.Property(e => e.X)
                .HasColumnName("x");

            builder.Property(e => e.Y)
                .HasColumnName("y");

            builder.Property(e => e.Order)
                .HasColumnName("order");

            builder.Property(e => e.BBoxId)
                .HasColumnName("bbox_id");

            builder.HasOne(e => e.BBox)
                .WithMany(e => e.Points)
                .HasForeignKey(e => e.BBoxId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
