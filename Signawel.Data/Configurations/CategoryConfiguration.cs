using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Signawel.Domain;

namespace Signawel.Data.Configurations
{
    class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("categories");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("id");

            builder.Property(e => e.OrderId)
                .HasColumnName("order_id")
                .IsRequired();

            builder.Property(e => e.Name)
                .IsUnicode()
                .HasColumnName("name")
                .IsRequired();

            builder.Property(e => e.ImagePath)
               .IsUnicode()
               .HasColumnName("image_path")
               .IsRequired();
        }
    }
}
