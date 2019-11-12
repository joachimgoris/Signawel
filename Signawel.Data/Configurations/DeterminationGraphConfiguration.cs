using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Signawel.Domain;

namespace Signawel.Data.Configurations
{
    public class DeterminationGraphConfiguration : IEntityTypeConfiguration<DeterminationGraph>
    {
        public void Configure(EntityTypeBuilder<DeterminationGraph> builder)
        {
            builder.ToTable("determination_graphs");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("determination_graph_id");

            builder.Property(e => e.StartId)
                .HasColumnName("start_node_id");

            builder.HasOne(e => e.Start)
                .WithOne()
                .HasForeignKey<DeterminationGraph>(e => e.StartId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
