using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Signawel.Domain;

namespace Signawel.Data.Configurations
{
    internal class ReportIssueConfiguration : IEntityTypeConfiguration<ReportIssue>
    {
        public void Configure(EntityTypeBuilder<ReportIssue> builder)
        {
            builder.ToTable("report_issues");

            builder.HasKey(e => new { e.ReportId, e.IssueId });

            builder.HasOne(e => e.Report)
                .WithMany(e => e.IssueLink)
                .HasForeignKey(e => e.ReportId);

            builder.HasOne(e => e.Issue)
                .WithMany(e => e.ReportLink)
                .HasForeignKey(e => e.IssueId);
        }
    }
}
