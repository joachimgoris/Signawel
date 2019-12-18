using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Signawel.Domain.ReportGroups;

namespace Signawel.Data.Configurations
{
    class UserReportGroupConfiguration : IEntityTypeConfiguration<UserReportGroup>
    {
        public void Configure(EntityTypeBuilder<UserReportGroup> builder)
        {
            builder.ToTable("user_report_group");

            builder.HasKey(crg => new { crg.UserId, crg.ReportGroupId });

            builder.HasOne(crg => crg.User)
                   .WithMany(c => c.UserReportGroups)
                   .HasForeignKey(crg => crg.UserId);

            builder.HasOne(crg => crg.ReportGroup)
                   .WithMany(r => r.UserReportGroups)
                   .HasForeignKey(crg => crg.ReportGroupId);


        }

    }
}


