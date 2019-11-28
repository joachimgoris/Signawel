using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Signawel.Domain.ReportGroups;

namespace Signawel.Data.Configurations
{
    public class EmailReportGroupConfiguration : IEntityTypeConfiguration<EmailReportGroup>
    {
        public void Configure(EntityTypeBuilder<EmailReportGroup> builder)
        {
            builder.ToTable("email_report_group");

            builder.HasKey(erg => new { erg.EmailId, erg.ReportGroupId });

            builder.HasOne(erg => erg.Email)
                   .WithMany(e => e.EmailReportGroups)
                   .HasForeignKey(erg => erg.EmailId);

            builder.HasOne(erg => erg.ReportGroup)
                   .WithMany(r => r.EmailReportGroups)
                   .HasForeignKey(erg => erg.ReportGroupId);


        }

    }
}
