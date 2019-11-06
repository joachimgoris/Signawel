using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Signawel.Domain;

namespace Signawel.Data.Configurations
{
    public class PriorityEmailConfiguration : IEntityTypeConfiguration<PriorityEmail>
    {
        public void Configure(EntityTypeBuilder<PriorityEmail> builder)
        {
            builder.ToTable("priority_emails");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.EmailSuffix)
                .IsUnicode()
                .IsRequired();
        }
    }
}
