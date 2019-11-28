using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Signawel.Domain.ReportGroups;

namespace Signawel.Data.Configurations
{
    public class CityReportGroupConfiguration : IEntityTypeConfiguration<CityReportGroup>
    {
        public void Configure(EntityTypeBuilder<CityReportGroup> builder)
        {
            builder.ToTable("city_report_group");

            builder.HasKey(crg => new { crg.CityId, crg.ReportGroupId });

            builder.HasOne(crg => crg.City)
                   .WithMany(c => c.CityReportGroups)
                   .HasForeignKey(crg => crg.CityId);

            builder.HasOne(crg => crg.ReportGroup)
                   .WithMany(r => r.CityReportGroups)
                   .HasForeignKey(crg => crg.ReportGroupId);


        }
    
    }
}
