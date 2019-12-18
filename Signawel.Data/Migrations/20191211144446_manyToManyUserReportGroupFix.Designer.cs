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
    [Migration("20191211144446_manyToManyUserReportGroupFix")]
    partial class manyToManyUserReportGroupFix
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

            modelBuilder.Entity("Signawel.Domain.BBox.BBox", b =>
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

            modelBuilder.Entity("Signawel.Domain.BBox.BBoxPoint", b =>
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

            modelBuilder.Entity("Signawel.Domain.Determination.DeterminationAnswer", b =>
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

            modelBuilder.Entity("Signawel.Domain.Determination.DeterminationGraph", b =>
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

            modelBuilder.Entity("Signawel.Domain.Determination.DeterminationNode", b =>
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

            modelBuilder.Entity("Signawel.Domain.ReportGroups.City", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.ToTable("cities");

                    b.HasData(
                        new
                        {
                            Id = "d7d9d52b-58bd-4735-9494-afb8338ced02",
                            Name = "Alken"
                        },
                        new
                        {
                            Id = "2ba99f21-ec5d-4716-a747-b6f9674cfed7",
                            Name = "As"
                        },
                        new
                        {
                            Id = "803ef2c9-e6c4-4ab2-b9a9-840676391193",
                            Name = "Beringen"
                        },
                        new
                        {
                            Id = "ae005474-62ad-4e88-b5e2-92aed6bcbd9d",
                            Name = "Bilzen"
                        },
                        new
                        {
                            Id = "db2254bd-8bee-4f7b-b483-f8eb99b217e8",
                            Name = "Bocholt"
                        },
                        new
                        {
                            Id = "09de62ab-6df8-4a83-8c49-d943ae92fa3c",
                            Name = "Borgloon"
                        },
                        new
                        {
                            Id = "a8873295-1b04-4cb0-9512-e8a125892ced",
                            Name = "Bree"
                        },
                        new
                        {
                            Id = "feb15f07-0a5d-4efe-bab7-bb2b0185af11",
                            Name = "Diepenbeek"
                        },
                        new
                        {
                            Id = "765b8fff-ba55-4937-86ec-54e895bcf445",
                            Name = "Dilsen-Stokkem"
                        },
                        new
                        {
                            Id = "6ab79ab5-4a25-4f70-a3a4-e6f9a2464085",
                            Name = "Genk"
                        },
                        new
                        {
                            Id = "a0c4e5a7-d3fd-4316-aa55-e2453039bf79",
                            Name = "Gingelom"
                        },
                        new
                        {
                            Id = "dc85cb5c-76b0-4377-9dd4-5682620679b2",
                            Name = "Halen"
                        },
                        new
                        {
                            Id = "d93a70d7-0051-41d8-98b5-5b1ef1f688dc",
                            Name = "Ham"
                        },
                        new
                        {
                            Id = "6f3e4e2a-1aba-4081-acc1-d9749d1c8deb",
                            Name = "Hamont-Achel"
                        },
                        new
                        {
                            Id = "63998eef-2229-49bd-be29-f3a5790e7627",
                            Name = "Hasselt"
                        },
                        new
                        {
                            Id = "40342a8d-e4c1-4e76-a6bd-5d00c693da16",
                            Name = "Hechelt-Eksel"
                        },
                        new
                        {
                            Id = "e7d0494c-851b-49ce-b7ad-1e89ab84ea1e",
                            Name = "Heers"
                        },
                        new
                        {
                            Id = "bc735323-5df4-46a4-a51e-3f841ef7967c",
                            Name = "Herk-de-Stad"
                        },
                        new
                        {
                            Id = "e75162b3-e71b-4430-9468-35271bd4f86f",
                            Name = "Herstappe"
                        },
                        new
                        {
                            Id = "7a9d7033-bc94-474d-8cb3-3bed13bf8d5a",
                            Name = "Heusden-Zolder"
                        },
                        new
                        {
                            Id = "c95a4d31-900f-47f1-b035-446f6d3dde27",
                            Name = "Hoeselt"
                        },
                        new
                        {
                            Id = "5851ea24-df2a-4c75-a2e6-cfb77128cc78",
                            Name = "Houthalen-Helchteren"
                        },
                        new
                        {
                            Id = "a8d376e5-b897-49d4-b981-bd9198e7bb0b",
                            Name = "Kinrooi"
                        },
                        new
                        {
                            Id = "71e9d76a-aee5-410c-baf2-67575d5b962d",
                            Name = "Kortessem"
                        },
                        new
                        {
                            Id = "30a6f7cc-c599-4f26-909e-27402f6dc720",
                            Name = "Lanaken"
                        },
                        new
                        {
                            Id = "84a2f31d-84fd-4a6b-b11d-b184dd455005",
                            Name = "Leopoldsburg"
                        },
                        new
                        {
                            Id = "353633e4-04d4-42ef-90c5-68ded2c9096d",
                            Name = "Lommel"
                        },
                        new
                        {
                            Id = "3fd4c8e0-0a3a-4f14-82a4-9e32d37a8e19",
                            Name = "Lummen"
                        },
                        new
                        {
                            Id = "716459e8-e0d4-47df-8a7a-cd19ec5b00e6",
                            Name = "Maaseik"
                        },
                        new
                        {
                            Id = "cdb81bcb-549a-4a2f-b886-2282bdbb5e0c",
                            Name = "Maasmechelen"
                        },
                        new
                        {
                            Id = "a1d384a3-e728-45ac-a899-5a580a65e05b",
                            Name = "Nieuwerkerken"
                        },
                        new
                        {
                            Id = "fd281172-e643-493f-b0ee-a9220e4a2f2b",
                            Name = "Oudsbergen"
                        },
                        new
                        {
                            Id = "bc476234-e317-4ee4-98d2-0b343460c27d",
                            Name = "Peer"
                        },
                        new
                        {
                            Id = "ec810334-63e8-4387-8422-0d2f4fb870f4",
                            Name = "Pelt"
                        },
                        new
                        {
                            Id = "53028f0e-3a23-4fa5-a5ae-e5cfe57ef0fb",
                            Name = "Riemst"
                        },
                        new
                        {
                            Id = "662c8e97-7e2e-4460-905f-c4be8981e0be",
                            Name = "Sint-Truiden"
                        },
                        new
                        {
                            Id = "43e53f7d-f942-4b82-94ac-032173544117",
                            Name = "Tessenderlo"
                        },
                        new
                        {
                            Id = "c4297e5e-a64c-4828-a3a6-4c80d8bc26fa",
                            Name = "Tongeren"
                        },
                        new
                        {
                            Id = "ad7839aa-7444-43e1-bc8e-aa04f196f48f",
                            Name = "Voeren"
                        },
                        new
                        {
                            Id = "f6afdaf0-4d5c-44b8-9cc2-6a2296155d78",
                            Name = "Wellen"
                        },
                        new
                        {
                            Id = "4b9eb36a-6aea-47b6-be83-16e8d9ccd29e",
                            Name = "Zonhoven"
                        },
                        new
                        {
                            Id = "07707203-2971-4329-a17e-75a22182a90e",
                            Name = "Zutendaal"
                        });
                });

            modelBuilder.Entity("Signawel.Domain.ReportGroups.CityReportGroup", b =>
                {
                    b.Property<string>("CityId");

                    b.Property<string>("ReportGroupId");

                    b.HasKey("CityId", "ReportGroupId");

                    b.HasIndex("ReportGroupId");

                    b.ToTable("city_report_group");
                });

            modelBuilder.Entity("Signawel.Domain.ReportGroups.Email", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnName("emailaddress")
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.ToTable("emails_reportgroups");
                });

            modelBuilder.Entity("Signawel.Domain.ReportGroups.EmailReportGroup", b =>
                {
                    b.Property<string>("EmailId");

                    b.Property<string>("ReportGroupId");

                    b.HasKey("EmailId", "ReportGroupId");

                    b.HasIndex("ReportGroupId");

                    b.ToTable("email_report_group");
                });

            modelBuilder.Entity("Signawel.Domain.ReportGroups.ReportGroup", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.HasKey("Id");

                    b.ToTable("reportgroups");
                });

            modelBuilder.Entity("Signawel.Domain.ReportGroups.UserReportGroup", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("ReportGroupId");

                    b.HasKey("UserId", "ReportGroupId");

                    b.HasIndex("ReportGroupId");

                    b.ToTable("user_report_group");
                });

            modelBuilder.Entity("Signawel.Domain.Reports.Report", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<DateTime>("CreationTime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("description")
                        .IsUnicode(true);

                    b.Property<string>("IssueId");

                    b.Property<string>("RoadworkId")
                        .IsRequired()
                        .HasColumnName("roadwork_id")
                        .IsUnicode(true);

                    b.Property<string>("SenderEmail")
                        .IsRequired()
                        .HasColumnName("sender_email")
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.HasIndex("IssueId");

                    b.ToTable("reports");
                });

            modelBuilder.Entity("Signawel.Domain.Reports.ReportDefaultIssue", b =>
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

                    b.ToTable("report_default_issues");
                });

            modelBuilder.Entity("Signawel.Domain.Reports.ReportImage", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("ImageId")
                        .IsRequired()
                        .HasColumnName("image_id");

                    b.Property<string>("ReportId")
                        .HasColumnName("report_id");

                    b.HasKey("Id");

                    b.HasIndex("ImageId");

                    b.HasIndex("ReportId");

                    b.ToTable("report_images");
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

                    b.Property<byte>("RoadworkCategory")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("category")
                        .HasDefaultValue((byte)0);

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

            modelBuilder.Entity("Signawel.Domain.BBox.BBox", b =>
                {
                    b.HasOne("Signawel.Domain.RoadworkSchema", "Schema")
                        .WithMany("BoundingBoxes")
                        .HasForeignKey("SchemaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Signawel.Domain.BBox.BBoxPoint", b =>
                {
                    b.HasOne("Signawel.Domain.BBox.BBox", "BBox")
                        .WithMany("Points")
                        .HasForeignKey("BBoxId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Signawel.Domain.Determination.DeterminationAnswer", b =>
                {
                    b.HasOne("Signawel.Domain.Determination.DeterminationNode", "Node")
                        .WithOne()
                        .HasForeignKey("Signawel.Domain.Determination.DeterminationAnswer", "NodeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Signawel.Domain.Determination.DeterminationNode", "ParentNode")
                        .WithMany("Answers")
                        .HasForeignKey("ParentNodeId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Signawel.Domain.Determination.DeterminationGraph", b =>
                {
                    b.HasOne("Signawel.Domain.Determination.DeterminationNode", "Start")
                        .WithOne()
                        .HasForeignKey("Signawel.Domain.Determination.DeterminationGraph", "StartId")
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

            modelBuilder.Entity("Signawel.Domain.ReportGroups.CityReportGroup", b =>
                {
                    b.HasOne("Signawel.Domain.ReportGroups.City", "City")
                        .WithMany("CityReportGroups")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Signawel.Domain.ReportGroups.ReportGroup", "ReportGroup")
                        .WithMany("CityReportGroups")
                        .HasForeignKey("ReportGroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Signawel.Domain.ReportGroups.EmailReportGroup", b =>
                {
                    b.HasOne("Signawel.Domain.ReportGroups.Email", "Email")
                        .WithMany("EmailReportGroups")
                        .HasForeignKey("EmailId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Signawel.Domain.ReportGroups.ReportGroup", "ReportGroup")
                        .WithMany("EmailReportGroups")
                        .HasForeignKey("ReportGroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Signawel.Domain.ReportGroups.UserReportGroup", b =>
                {
                    b.HasOne("Signawel.Domain.ReportGroups.ReportGroup", "ReportGroup")
                        .WithMany("UserReportGroups")
                        .HasForeignKey("ReportGroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Signawel.Domain.User", "User")
                        .WithMany("UserReportGroups")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Signawel.Domain.Reports.Report", b =>
                {
                    b.HasOne("Signawel.Domain.Reports.ReportDefaultIssue", "Issue")
                        .WithMany()
                        .HasForeignKey("IssueId");
                });

            modelBuilder.Entity("Signawel.Domain.Reports.ReportImage", b =>
                {
                    b.HasOne("Signawel.Domain.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Signawel.Domain.Reports.Report", "Report")
                        .WithMany("Images")
                        .HasForeignKey("ReportId");
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
