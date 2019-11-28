using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Signawel.Domain;

namespace Signawel.Data.Configurations
{
    public class RoadworkSchemaConfiguration : IEntityTypeConfiguration<RoadworkSchema>
    {
        public void Configure(EntityTypeBuilder<RoadworkSchema> builder)
        {
            builder.ToTable("roadwork_schemas");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("schema_id");

            builder.Property(e => e.Name)
                .HasColumnName("name");

            builder.Property(e => e.ImageId)
                .HasColumnName("image_id");

            builder.HasOne(e => e.Image)
                .WithMany()
                .HasForeignKey(e => e.ImageId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.BoundingBoxes)
                .WithOne(e => e.Schema)
                .HasForeignKey(e => e.SchemaId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
