﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Signawel.Data;

namespace Signawel.Data.Migrations
{
    [DbContext(typeof(SignawelDbContext))]
    partial class SignawelDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("Signawel.Domain.Category", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasColumnName("image_path")
                        .IsUnicode(true);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnName("name")
                        .IsUnicode(true);

                    b.Property<int>("OrderId")
                        .HasColumnName("order_id");

                    b.HasKey("Id");

                    b.ToTable("categories");
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

                    b.Property<string>("Cities")
                        .HasColumnName("cities");

                    b.Property<string>("CustomMessage")
                        .HasColumnName("custom_message")
                        .IsUnicode(true);

                    b.Property<string>("Description")
                        .HasColumnName("description")
                        .IsUnicode(true);

                    b.Property<string>("RoadWorkId")
                        .HasColumnName("roadwork_id")
                        .IsUnicode(true);

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
                            Id = "8a633419-800e-4a89-a961-ffca6b345674",
                            Name = "Alken"
                        },
                        new
                        {
                            Id = "d55f82c4-fc9c-4b4e-88fd-492435720714",
                            Name = "As"
                        },
                        new
                        {
                            Id = "53f64ddf-1033-4605-8da6-891ed22482f8",
                            Name = "Beringen"
                        },
                        new
                        {
                            Id = "a84979fb-1bda-4ead-90e0-85b97d26c6af",
                            Name = "Bilzen"
                        },
                        new
                        {
                            Id = "fff05f3c-cba1-4a62-a85d-2333284ed993",
                            Name = "Bocholt"
                        },
                        new
                        {
                            Id = "67e0b43b-473e-491e-b3e7-15df1ec7ae3c",
                            Name = "Borgloon"
                        },
                        new
                        {
                            Id = "6577c629-cc7b-491b-b6c6-f4d7c023ab1c",
                            Name = "Bree"
                        },
                        new
                        {
                            Id = "006a8dc0-fc62-494d-873e-1bd9c1a1d6e7",
                            Name = "Diepenbeek"
                        },
                        new
                        {
                            Id = "8a101db6-0374-4735-9a50-35efab98b28d",
                            Name = "Dilsen-Stokkem"
                        },
                        new
                        {
                            Id = "a876ad88-5177-4792-8928-f2e34291b758",
                            Name = "Genk"
                        },
                        new
                        {
                            Id = "86a933cc-f513-42ca-916a-54896382cf6c",
                            Name = "Gingelom"
                        },
                        new
                        {
                            Id = "51966a26-0abd-4380-b895-175a51bba1cc",
                            Name = "Halen"
                        },
                        new
                        {
                            Id = "e996e794-bcaa-4ebc-ba4e-b7e31ccff685",
                            Name = "Ham"
                        },
                        new
                        {
                            Id = "7b1281e7-5b37-47a3-a518-e11e55b99d97",
                            Name = "Hamont-Achel"
                        },
                        new
                        {
                            Id = "b9050566-019c-4b9f-80f3-1116f78048c7",
                            Name = "Hasselt"
                        },
                        new
                        {
                            Id = "a15341cd-8d96-41d4-8dd7-9a3e83e9f680",
                            Name = "Hechelt-Eksel"
                        },
                        new
                        {
                            Id = "2dd0adf4-2a8e-476f-b34b-0f953519a4f9",
                            Name = "Heers"
                        },
                        new
                        {
                            Id = "0df34ea2-13af-4da8-adf6-97a88928f218",
                            Name = "Herk-de-Stad"
                        },
                        new
                        {
                            Id = "654c508c-732a-460c-bb7e-4fab63923063",
                            Name = "Herstappe"
                        },
                        new
                        {
                            Id = "b11c9dc6-b4df-4943-b374-3bc60483f4a4",
                            Name = "Heusden-Zolder"
                        },
                        new
                        {
                            Id = "26533f8e-c0e6-4ee7-95bf-ea3c23e6b28d",
                            Name = "Hoeselt"
                        },
                        new
                        {
                            Id = "2d427602-340c-4aff-b142-bae57b6056d7",
                            Name = "Houthalen-Helchteren"
                        },
                        new
                        {
                            Id = "5339b928-01f4-4338-bc79-4c02fdc26bdb",
                            Name = "Kinrooi"
                        },
                        new
                        {
                            Id = "d9df5550-2d74-4ac9-a2b7-8afceb0217c5",
                            Name = "Kortessem"
                        },
                        new
                        {
                            Id = "47393c8a-5e56-4040-b79d-671daff47dc4",
                            Name = "Lanaken"
                        },
                        new
                        {
                            Id = "ac330e5e-92d6-4a94-a018-95df1ba1fb16",
                            Name = "Leopoldsburg"
                        },
                        new
                        {
                            Id = "6f333ad3-fe7f-429e-b085-6092a499f92a",
                            Name = "Lommel"
                        },
                        new
                        {
                            Id = "937253ed-3ff4-4b44-8a56-a262a27720da",
                            Name = "Lummen"
                        },
                        new
                        {
                            Id = "9042fccf-1ee5-4341-9654-f1ac90c81721",
                            Name = "Maaseik"
                        },
                        new
                        {
                            Id = "7f3db7c7-7cfb-4f84-bd97-4f6240110607",
                            Name = "Maasmechelen"
                        },
                        new
                        {
                            Id = "c3e3c6b8-e702-4e0e-82d1-c4bd66f10888",
                            Name = "Nieuwerkerken"
                        },
                        new
                        {
                            Id = "f292ee1d-545d-4349-8f76-e87cc51b12eb",
                            Name = "Oudsbergen"
                        },
                        new
                        {
                            Id = "ba4a158e-b3c3-4f17-b8d8-ee7954110eb0",
                            Name = "Peer"
                        },
                        new
                        {
                            Id = "c0dd9178-c926-4c3e-9ef6-55afa677cec0",
                            Name = "Pelt"
                        },
                        new
                        {
                            Id = "27f067bf-0d16-442a-ae65-7eace45d2756",
                            Name = "Riemst"
                        },
                        new
                        {
                            Id = "8cf938dc-e948-4b0a-ab15-d380f8bd3b3e",
                            Name = "Sint-Truiden"
                        },
                        new
                        {
                            Id = "03405f9e-21a1-4d32-a674-9c49f9c25cb8",
                            Name = "Tessenderlo"
                        },
                        new
                        {
                            Id = "95614355-c6cb-4ba2-bc53-f79bca66d6f7",
                            Name = "Tongeren"
                        },
                        new
                        {
                            Id = "e2ebab08-9fa4-44d3-af65-4248ac313bae",
                            Name = "Voeren"
                        },
                        new
                        {
                            Id = "f26c139b-2ce9-4ddb-938c-43bcae8c65b3",
                            Name = "Wellen"
                        },
                        new
                        {
                            Id = "2531b6c9-a173-4105-92b3-ba51a1b4ac52",
                            Name = "Zonhoven"
                        },
                        new
                        {
                            Id = "e6e04d27-b61b-40ea-8f3f-9905b969a021",
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
