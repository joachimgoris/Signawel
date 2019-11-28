using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Signawel.Domain;

namespace Signawel.Data.Configurations
{
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.ToTable("images");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("image_id");

            builder.Property(e => e.ImagePath)
                .HasColumnName("image_path");
        }
    }
}
