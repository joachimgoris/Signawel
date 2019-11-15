﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Signawel.Data;

namespace Signawel.Data.Migrations
{
    [DbContext(typeof(SignawelDbContext))]
    [Migration("20191114084051_InitialLogin")]
    partial class InitialLogin
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Signawel.Domain.BBox", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("bbox_id");

                    b.Property<string>("Name")
                        .HasColumnName("name");

                    b.Property<string>("SchemaId")
                        .HasColumnName("schema_id");

                    b.HasKey("Id");

                    b.HasIndex("SchemaId");

                    b.ToTable("bboxes");
                });

            modelBuilder.Entity("Signawel.Domain.BBoxPoint", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("point_id");

                    b.Property<string>("BBoxId")
                        .HasColumnName("bbox_id");

                    b.Property<int>("Order")
                        .HasColumnName("order");

                    b.Property<double>("X")
                        .HasColumnName("x");

                    b.Property<double>("Y")
                        .HasColumnName("y");

                    b.HasKey("Id");

                    b.HasIndex("BBoxId");

                    b.ToTable("bbox_points");
                });

            modelBuilder.Entity("Signawel.Domain.DefaultIssue", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .IsUnicode(true);

                    b.Property<int>("Type")
                        .HasColumnName("type");

                    b.HasKey("Id");

                    b.ToTable("default_issues");
                });

            modelBuilder.Entity("Signawel.Domain.DeterminationAnswer", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("answer_id");

                    b.Property<string>("Answer")
                        .IsRequired()
                        .HasColumnName("answer");

                    b.Property<string>("NodeId")
                        .HasColumnName("node_id");

                    b.Property<string>("ParentNodeId")
                        .HasColumnName("parent_node_id");

                    b.HasKey("Id");

                    b.HasIndex("NodeId")
                        .IsUnique()
                        .HasFilter("[node_id] IS NOT NULL");

                    b.HasIndex("ParentNodeId");

                    b.ToTable("determination_graph_answers");
                });

            modelBuilder.Entity("Signawel.Domain.DeterminationGraph", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("determination_graph_id");

                    b.Property<string>("StartId")
                        .HasColumnName("start_node_id");

                    b.HasKey("Id");

                    b.HasIndex("StartId")
                        .IsUnique()
                        .HasFilter("[start_node_id] IS NOT NULL");

                    b.ToTable("determination_graphs");
                });

            modelBuilder.Entity("Signawel.Domain.DeterminationNode", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("node_id");

                    b.Property<string>("Question")
                        .HasColumnName("question");

                    b.Property<string>("QuestionDescription");

                    b.Property<string>("SchemaId")
                        .HasColumnName("schema_id");

                    b.Property<byte>("Type")
                        .HasColumnName("type");

                    b.HasKey("Id");

                    b.ToTable("determination_graph_nodes");
                });

            modelBuilder.Entity("Signawel.Domain.Image", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("image_id");

                    b.Property<string>("ImagePath")
                        .HasColumnName("image_path");

                    b.HasKey("Id");

                    b.ToTable("images");
                });

            modelBuilder.Entity("Signawel.Domain.LoginRecord", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("IpAddress")
                        .HasColumnName("ip_address");

                    b.Property<bool>("Succes")
                        .HasColumnName("succes");

                    b.Property<string>("UserId")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("login_records");
                });

            modelBuilder.Entity("Signawel.Domain.PriorityEmail", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("EmailSuffix")
                        .IsRequired()
                        .HasColumnName("email_suffix")
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.ToTable("priority_emails");
                });

            modelBuilder.Entity("Signawel.Domain.RefreshToken", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnName("expiry_date");

                    b.Property<bool>("Invalidated");

                    b.Property<string>("JwtId")
                        .IsRequired()
                        .HasColumnName("jwt_id");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnName("token");

                    b.Property<bool>("Used");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("refresh_tokens");
                });

            modelBuilder.Entity("Signawel.Domain.Report", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("CustomMessage")
                        .HasColumnName("custom_message")
                        .IsUnicode(true);

                    b.Property<bool>("Priority")
                        .HasColumnName("priority");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnName("user_email")
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.ToTable("reports");
                });

            modelBuilder.Entity("Signawel.Domain.ReportImage", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnName("image_path");

                    b.Property<string>("ReportId")
                        .HasColumnName("report_id");

                    b.HasKey("Id");

                    b.HasIndex("ReportId");

                    b.ToTable("report_images");
                });

            modelBuilder.Entity("Signawel.Domain.ReportIssue", b =>
                {
                    b.Property<string>("ReportId");

                    b.Property<string>("IssueId");

                    b.HasKey("ReportId", "IssueId");

                    b.HasIndex("IssueId");

                    b.ToTable("report_issues");
                });

            modelBuilder.Entity("Signawel.Domain.RoadworkSchema", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("schema_id");

                    b.Property<string>("ImageId")
                        .HasColumnName("image_id");

                    b.Property<string>("Name")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.ToTable("roadwork_schemas");
                });

            modelBuilder.Entity("Signawel.Domain.Role", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Signawel.Domain.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Signawel.Domain.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Signawel.Domain.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Signawel.Domain.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Signawel.Domain.Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Signawel.Domain.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Signawel.Domain.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Signawel.Domain.BBox", b =>
                {
                    b.HasOne("Signawel.Domain.RoadworkSchema", "Schema")
                        .WithMany("BoundingBoxes")
                        .HasForeignKey("SchemaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Signawel.Domain.BBoxPoint", b =>
                {
                    b.HasOne("Signawel.Domain.BBox", "BBox")
                        .WithMany("Points")
                        .HasForeignKey("BBoxId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Signawel.Domain.DeterminationAnswer", b =>
                {
                    b.HasOne("Signawel.Domain.DeterminationNode", "Node")
                        .WithOne()
                        .HasForeignKey("Signawel.Domain.DeterminationAnswer", "NodeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Signawel.Domain.DeterminationNode", "ParentNode")
                        .WithMany("Answers")
                        .HasForeignKey("ParentNodeId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Signawel.Domain.DeterminationGraph", b =>
                {
                    b.HasOne("Signawel.Domain.DeterminationNode", "Start")
                        .WithOne()
                        .HasForeignKey("Signawel.Domain.DeterminationGraph", "StartId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Signawel.Domain.LoginRecord", b =>
                {
                    b.HasOne("Signawel.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Signawel.Domain.RefreshToken", b =>
                {
                    b.HasOne("Signawel.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Signawel.Domain.ReportImage", b =>
                {
                    b.HasOne("Signawel.Domain.Report", "Report")
                        .WithMany("Images")
                        .HasForeignKey("ReportId");
                });

            modelBuilder.Entity("Signawel.Domain.ReportIssue", b =>
                {
                    b.HasOne("Signawel.Domain.DefaultIssue", "Issue")
                        .WithMany("ReportLink")
                        .HasForeignKey("IssueId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Signawel.Domain.Report", "Report")
                        .WithMany("IssueLink")
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Signawel.Domain.RoadworkSchema", b =>
                {
                    b.HasOne("Signawel.Domain.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
