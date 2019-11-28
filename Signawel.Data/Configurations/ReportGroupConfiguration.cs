using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Signawel.Domain.ReportGroups;

namespace Signawel.Data.Configurations
{
    public class ReportGroupConfiguration : IEntityTypeConfiguration<ReportGroup>
    {
        public void Configure(EntityTypeBuilder<ReportGroup> builder)
        {
            builder.ToTable("reportgroups");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id).HasColumnName("id");

        }
    }
}
