using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Signawel.Domain;

namespace Signawel.Data.Configurations
{
    internal class DefaultIssueConfiguration : IEntityTypeConfiguration<DefaultIssue>
    {
        public void Configure(EntityTypeBuilder<DefaultIssue> builder)
        {
            builder.ToTable("default_issues");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName("id");
            builder.Property(e => e.Type).HasColumnName("type");
            builder.Property(e => e.Name)
                .IsUnicode()
                .IsRequired()
                .HasColumnName("name");
        }
    }
}
