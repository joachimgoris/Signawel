using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Signawel.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Signawel.Domain.BBox;

namespace Signawel.Data.Configurations
{
    public class BBoxConfiguration : IEntityTypeConfiguration<BBox>
    {
        public void Configure(EntityTypeBuilder<BBox> builder)
        {
            builder.ToTable("bboxes");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("bbox_id");

            builder.Property(e => e.Name)
                .HasColumnName("name");

            builder.Property(e => e.SchemaId)
                .HasColumnName("schema_id");

            builder.HasMany(e => e.Points)
                .WithOne(e => e.BBox)
                .HasForeignKey(e => e.BBoxId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Schema)
                .WithMany(e => e.BoundingBoxes)
                .HasForeignKey(e => e.SchemaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
