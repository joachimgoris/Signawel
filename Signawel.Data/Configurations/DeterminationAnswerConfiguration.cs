using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Signawel.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Signawel.Data.Configurations
{
    class DeterminationAnswerConfiguration : IEntityTypeConfiguration<DeterminationAnswer>
    {
        public void Configure(EntityTypeBuilder<DeterminationAnswer> builder)
        {
            builder.ToTable("determination_graph_answers");

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id)
                .HasColumnName("answer_id");

            builder.Property(e => e.Answer)
                .IsRequired()
                .HasColumnName("answer");

            builder.Property(e => e.NodeId)
                .HasColumnName("node_id");

            builder.Property(e => e.ParentNodeId)
                .HasColumnName("parent_node_id");

            builder.HasOne(e => e.Node)
                .WithOne()
                .HasForeignKey<DeterminationAnswer>(e => e.NodeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.ParentNode)
                .WithMany(e => e.Answers)
                .HasForeignKey(e => e.ParentNodeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
