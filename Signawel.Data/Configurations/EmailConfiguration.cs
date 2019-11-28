using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Signawel.Domain.ReportGroups;

namespace Signawel.Data.Configurations
{
    public class EmailConfiguration : IEntityTypeConfiguration<Email>
    {
        public void Configure(EntityTypeBuilder<Email> builder)
        {
            builder.ToTable("emails_reportgroups");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.EmailAddress)
                .IsUnicode()
                .HasColumnName("emailaddress")
                .IsRequired();

        }

    }
}
