using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Signawel.Domain;
using Signawel.Domain.Reports;

namespace Signawel.Data.Configurations
{
    internal class ReportDefaultIssueConfiguration : IEntityTypeConfiguration<ReportDefaultIssue>
    {
        public void Configure(EntityTypeBuilder<ReportDefaultIssue> builder)
        {
            builder.ToTable("report_default_issues");

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
