using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Signawel.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Signawel.Data.Configurations
{
    public class DeterminationNodeConfiguration : IEntityTypeConfiguration<DeterminationNode>
    {
        public void Configure(EntityTypeBuilder<DeterminationNode> builder)
        {
            builder.ToTable("determination_graph_nodes");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("node_id");

            builder.Property(e => e.Question)
                .HasColumnName("question");

            builder.Property(e => e.SchemaId)
                .HasColumnName("schema_id");

            builder.Property(e => e.Type)
                .HasColumnName("type");

            builder.HasMany(e => e.Answers)
                .WithOne(e => e.ParentNode)
                .HasForeignKey(e => e.ParentNodeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
