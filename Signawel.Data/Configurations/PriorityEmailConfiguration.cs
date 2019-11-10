using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Signawel.Domain;

namespace Signawel.Data.Configurations
{
    internal class PriorityEmailConfiguration : IEntityTypeConfiguration<PriorityEmail>
    {
        public void Configure(EntityTypeBuilder<PriorityEmail> builder)
        {
            builder.ToTable("priority_emails");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.EmailSuffix)
                .IsUnicode()
                .IsRequired()
                .HasColumnName("email_suffix");
        }
    }
}
