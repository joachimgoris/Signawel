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
    [Migration("20191122150442_CitiesAdded")]
    partial class CitiesAdded
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
                            Id = "10ae4cbb-da20-46dd-a5c7-e84ac154ec5c",
                            Name = "Alken"
                        },
                        new
                        {
                            Id = "ba15685c-21ef-43ba-b137-2bf6dcb6014b",
                            Name = "As"
                        },
                        new
                        {
                            Id = "b6eaea68-96cc-42e0-af25-50a4d91f1827",
                            Name = "Beringen"
                        },
                        new
                        {
                            Id = "f4a3c949-aa86-42ec-b411-f38793f9eb7a",
                            Name = "Bilzen"
                        },
                        new
                        {
                            Id = "fc01fd7a-5d4a-4365-b8f3-2d72f05054f0",
                            Name = "Bocholt"
                        },
                        new
                        {
                            Id = "ea26e298-8638-420a-a66e-342b56c19479",
                            Name = "Borgloon"
                        },
                        new
                        {
                            Id = "982ad41a-c655-4406-9c00-b1c8b10d9591",
                            Name = "Bree"
                        },
                        new
                        {
                            Id = "43b03c97-6cd1-4348-a7c9-0a7c9257f8a1",
                            Name = "Diepenbeek"
                        },
                        new
                        {
                            Id = "178bae7e-a3f2-4a74-800d-ba76729455e6",
                            Name = "Dilsen-Stokkem"
                        },
                        new
                        {
                            Id = "0d702a37-aff8-4762-9d52-47e4032bd1b7",
                            Name = "Genk"
                        },
                        new
                        {
                            Id = "0970f669-30a5-4ce0-9ce0-634c7211d556",
                            Name = "Gingelom"
                        },
                        new
                        {
                            Id = "5f41fbc7-a5c0-4cd5-a543-d2d2b3540916",
                            Name = "Halen"
                        },
                        new
                        {
                            Id = "646a4dc7-32d6-43bd-abc5-39b91e33ceb2",
                            Name = "Ham"
                        },
                        new
                        {
                            Id = "d7be3170-1168-4bb1-808e-ea4327f0d18d",
                            Name = "Hamont-Achel"
                        },
                        new
                        {
                            Id = "2ef35437-1f5c-4a4b-bf89-0cf2cc462c1e",
                            Name = "Hasselt"
                        },
                        new
                        {
                            Id = "4cb665e3-20c0-44f4-a4ba-27209df8780e",
                            Name = "Hechelt-Eksel"
                        },
                        new
                        {
                            Id = "3397df4f-d8d7-4204-aa14-4933ea6a566d",
                            Name = "Heers"
                        },
                        new
                        {
                            Id = "1d9a7c95-d352-44c6-a44c-2796b7f145a3",
                            Name = "Herk-de-Stad"
                        },
                        new
                        {
                            Id = "7120c3e6-65b0-4e09-b1a2-4c68f0c37c61",
                            Name = "Herstappe"
                        },
                        new
                        {
                            Id = "e3d21daf-77dc-4c10-8b76-45b947aeb00b",
                            Name = "Heusden-Zolder"
                        },
                        new
                        {
                            Id = "a88262ca-9849-4488-b3df-083160419ce4",
                            Name = "Hoeselt"
                        },
                        new
                        {
                            Id = "c1d7ab62-c2b5-43bd-a1e6-293178968db3",
                            Name = "Houthalen-Helchteren"
                        },
                        new
                        {
                            Id = "44857e82-ea0b-4125-8aad-f523e4550098",
                            Name = "Kinrooi"
                        },
                        new
                        {
                            Id = "1c08220f-8b1b-4a29-b6a1-e0e206316368",
                            Name = "Kortessem"
                        },
                        new
                        {
                            Id = "388a8d5d-31c6-4c41-aece-1c84d8964fe8",
                            Name = "Lanaken"
                        },
                        new
                        {
                            Id = "5d409b28-276f-4c79-93bf-eb3c05c2b2e9",
                            Name = "Leopoldsburg"
                        },
                        new
                        {
                            Id = "89a075c1-2871-4f82-87ce-05d8ab71bee4",
                            Name = "Lommel"
                        },
                        new
                        {
                            Id = "b8fb2a7a-4c08-480f-b44f-06af4391dae0",
                            Name = "Lummen"
                        },
                        new
                        {
                            Id = "75f62a96-5d9a-4302-8878-cdf166bd420d",
                            Name = "Maaseik"
                        },
                        new
                        {
                            Id = "0816a85c-cf1b-4098-a627-6c2c2be21a44",
                            Name = "Maasmechelen"
                        },
                        new
                        {
                            Id = "947dac05-622e-465c-a0c9-fffc51c116c7",
                            Name = "Nieuwerkerken"
                        },
                        new
                        {
                            Id = "b6c2d29c-b490-4049-b83b-aa180d1b64ed",
                            Name = "Oudsbergen"
                        },
                        new
                        {
                            Id = "eff66d7f-2204-43f0-8f9e-5251e0231ddc",
                            Name = "Peer"
                        },
                        new
                        {
                            Id = "874c82f6-dbf7-44b1-a1a1-fd0ed5038b6d",
                            Name = "Pelt"
                        },
                        new
                        {
                            Id = "abda5417-31c7-4310-b5d1-1e0856468085",
                            Name = "Riemst"
                        },
                        new
                        {
                            Id = "c54f182d-bbca-4c98-bdcc-652f2356f801",
                            Name = "Sint-Truiden"
                        },
                        new
                        {
                            Id = "fb2d071e-c077-47e6-9052-e0b30789f8f9",
                            Name = "Tessenderlo"
                        },
                        new
                        {
                            Id = "775f3629-d176-4128-9d02-43ea8aa0f3ae",
                            Name = "Tongeren"
                        },
                        new
                        {
                            Id = "f40240d0-f8b0-4d6b-8eb5-3af5cbdf2d3b",
                            Name = "Voeren"
                        },
                        new
                        {
                            Id = "f6cd4777-90e5-425f-990c-3b16f629b367",
                            Name = "Wellen"
                        },
                        new
                        {
                            Id = "d2bd75ea-048e-4d3a-b804-1de5d4c381b5",
                            Name = "Zonhoven"
                        },
                        new
                        {
                            Id = "a26462fd-7a7e-4dd1-abfb-5c9a3d6bcf20",
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
