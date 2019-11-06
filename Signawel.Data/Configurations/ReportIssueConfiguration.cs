using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Signawel.Domain;

namespace Signawel.Data.Configurations
{
    public class ReportIssueConfiguration : IEntityTypeConfiguration<ReportIssue>
    {
        public void Configure(EntityTypeBuilder<ReportIssue> builder)
        {
            builder.HasOne(e => e.Report)
                .WithMany(e => e.IssueLink)
                .HasForeignKey(e => e.ReportId);

            builder.HasOne(e => e.Issue)
                .WithMany(e => e.ReportLink)
                .HasForeignKey(e => e.IssueId);
        }
    }
}
