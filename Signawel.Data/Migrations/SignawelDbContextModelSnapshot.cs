﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

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

            modelBuilder.Entity("Signawel.Domain.BlacklistEmail", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("email")
                        .IsUnicode(true);

                    b.HasKey("Id");

                    b.ToTable("blacklist_emails");
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
                            Id = "a746afca-5745-4550-b295-4f74205b30c6",
                            Name = "Alken"
                        },
                        new
                        {
                            Id = "317e845c-0a3d-43c4-a641-2f12fc14f2b3",
                            Name = "As"
                        },
                        new
                        {
                            Id = "aeb31ddd-a6fd-4aa2-8ec4-6d9d968a7adc",
                            Name = "Beringen"
                        },
                        new
                        {
                            Id = "314c50d9-9508-4846-b11e-2fac56400993",
                            Name = "Bilzen"
                        },
                        new
                        {
                            Id = "e09576f1-4ba1-4e32-becd-0cf1a534282d",
                            Name = "Bocholt"
                        },
                        new
                        {
                            Id = "26b7c0ad-84e6-4a8f-b0eb-de3fcec49eb8",
                            Name = "Borgloon"
                        },
                        new
                        {
                            Id = "efdc9ae3-7ba9-41b1-97e9-d3ca3a0a0248",
                            Name = "Bree"
                        },
                        new
                        {
                            Id = "ebfe45cd-5986-4fd0-9802-8e916ff98212",
                            Name = "Diepenbeek"
                        },
                        new
                        {
                            Id = "623c2b2b-b279-413d-818e-96aca1e932fe",
                            Name = "Dilsen-Stokkem"
                        },
                        new
                        {
                            Id = "fe3f3c63-3191-43b9-a5d8-538fd26b52ca",
                            Name = "Genk"
                        },
                        new
                        {
                            Id = "3b142810-ecbe-475b-bf19-c611932ed930",
                            Name = "Gingelom"
                        },
                        new
                        {
                            Id = "60dcaa42-0b7b-499a-b18f-904b17470699",
                            Name = "Halen"
                        },
                        new
                        {
                            Id = "7fe186a3-3434-4e71-9ee4-e9792fca2229",
                            Name = "Ham"
                        },
                        new
                        {
                            Id = "8c913824-7771-45db-80f8-8857def8f96a",
                            Name = "Hamont-Achel"
                        },
                        new
                        {
                            Id = "7c66efbe-c3b5-40f7-9d0b-8b268bac7079",
                            Name = "Hasselt"
                        },
                        new
                        {
                            Id = "d3a271b9-fd78-4164-8040-223b4c1952ab",
                            Name = "Hechelt-Eksel"
                        },
                        new
                        {
                            Id = "22cd04ae-d056-4efd-8ae5-9c2092642baf",
                            Name = "Heers"
                        },
                        new
                        {
                            Id = "97ae0fd2-05a2-48a4-95fe-f67eee31eace",
                            Name = "Herk-de-Stad"
                        },
                        new
                        {
                            Id = "8478d77d-88f9-479c-8d10-f4fe18160141",
                            Name = "Herstappe"
                        },
                        new
                        {
                            Id = "3cb40fc4-89be-4d80-b78f-2206da61ea5a",
                            Name = "Heusden-Zolder"
                        },
                        new
                        {
                            Id = "bab4b67a-0bfb-4a60-8ec0-bd8ff64070dd",
                            Name = "Hoeselt"
                        },
                        new
                        {
                            Id = "abdf0127-1a21-4653-860c-dc5c33c1a9aa",
                            Name = "Houthalen-Helchteren"
                        },
                        new
                        {
                            Id = "e748cc0c-d6e7-49ac-beb1-33aa5a291eeb",
                            Name = "Kinrooi"
                        },
                        new
                        {
                            Id = "de1c08d1-dc66-4e8e-b06f-8332cde85858",
                            Name = "Kortessem"
                        },
                        new
                        {
                            Id = "54f3ece6-c126-40b2-a420-296602c27fa3",
                            Name = "Lanaken"
                        },
                        new
                        {
                            Id = "ab302936-3497-417d-a82e-17a6d5b55c63",
                            Name = "Leopoldsburg"
                        },
                        new
                        {
                            Id = "3fb78ac3-f2a8-4c27-96c7-18d62403627e",
                            Name = "Lommel"
                        },
                        new
                        {
                            Id = "71cda990-7ab7-4ea1-b889-97997132c03c",
                            Name = "Lummen"
                        },
                        new
                        {
                            Id = "3a3f4b84-e2a9-49ef-939a-303f4755f732",
                            Name = "Maaseik"
                        },
                        new
                        {
                            Id = "dcf40d83-e9dd-4b10-81fd-427bda6b6519",
                            Name = "Maasmechelen"
                        },
                        new
                        {
                            Id = "65309130-a883-43c7-b5ec-8d3502f8debf",
                            Name = "Nieuwerkerken"
                        },
                        new
                        {
                            Id = "9e5a5481-a457-4c15-a846-4105ba06c773",
                            Name = "Oudsbergen"
                        },
                        new
                        {
                            Id = "57820467-127e-4188-b1cf-d42252d59a49",
                            Name = "Peer"
                        },
                        new
                        {
                            Id = "22c765ba-9a80-40ed-bfd8-8687df5b623c",
                            Name = "Pelt"
                        },
                        new
                        {
                            Id = "193cc9da-bf93-4004-a3a6-96e315c61304",
                            Name = "Riemst"
                        },
                        new
                        {
                            Id = "88f7c08b-d246-4e79-bdec-54fc2ce6ea75",
                            Name = "Sint-Truiden"
                        },
                        new
                        {
                            Id = "ff93b834-7b20-45a8-a305-ba42dd6cd389",
                            Name = "Tessenderlo"
                        },
                        new
                        {
                            Id = "66ec2064-d0c6-4695-8f29-56e619b393a3",
                            Name = "Tongeren"
                        },
                        new
                        {
                            Id = "9153d263-81df-4793-9056-0cc7f1589c3e",
                            Name = "Voeren"
                        },
                        new
                        {
                            Id = "a9482e53-2fd8-44f5-9197-ee1b5c462481",
                            Name = "Wellen"
                        },
                        new
                        {
                            Id = "cbd1b672-c8c8-4adf-aeae-0d60768dc5d7",
                            Name = "Zonhoven"
                        },
                        new
                        {
                            Id = "bf73754b-cef5-4748-8b26-f778b2e3e38b",
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
