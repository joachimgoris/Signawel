using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Signawel.Data.Migrations
{
    public partial class Reset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "blacklist_emails",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    email = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blacklist_emails", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cities",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "determination_graph_nodes",
                columns: table => new
                {
                    node_id = table.Column<string>(nullable: false),
                    type = table.Column<byte>(nullable: false),
                    question = table.Column<string>(nullable: true),
                    QuestionDescription = table.Column<string>(nullable: true),
                    schema_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_determination_graph_nodes", x => x.node_id);
                });

            migrationBuilder.CreateTable(
                name: "emails_reportgroups",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    emailaddress = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_emails_reportgroups", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "images",
                columns: table => new
                {
                    image_id = table.Column<string>(nullable: false),
                    image_path = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_images", x => x.image_id);
                });

            migrationBuilder.CreateTable(
                name: "priority_emails",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    email_suffix = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_priority_emails", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "report_default_issues",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_report_default_issues", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "reportgroups",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reportgroups", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "reports",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    creation_time = table.Column<DateTime>(nullable: false),
                    sender_email = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: true),
                    roadwork_id = table.Column<string>(nullable: false),
                    issue_id = table.Column<string>(nullable: true),
                    cities = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reports", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "login_records",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    user_id = table.Column<string>(nullable: true),
                    ip_address = table.Column<string>(nullable: true),
                    succes = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_login_records", x => x.id);
                    table.ForeignKey(
                        name: "FK_login_records_AspNetUsers_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "refresh_tokens",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    token = table.Column<string>(nullable: false),
                    jwt_id = table.Column<string>(nullable: false),
                    expiry_date = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Used = table.Column<bool>(nullable: false),
                    Invalidated = table.Column<bool>(nullable: false),
                    user_id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_refresh_tokens", x => x.id);
                    table.ForeignKey(
                        name: "FK_refresh_tokens_AspNetUsers_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "determination_graph_answers",
                columns: table => new
                {
                    answer_id = table.Column<string>(nullable: false),
                    answer = table.Column<string>(nullable: false),
                    node_id = table.Column<string>(nullable: true),
                    parent_node_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_determination_graph_answers", x => x.answer_id);
                    table.ForeignKey(
                        name: "FK_determination_graph_answers_determination_graph_nodes_node_id",
                        column: x => x.node_id,
                        principalTable: "determination_graph_nodes",
                        principalColumn: "node_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_determination_graph_answers_determination_graph_nodes_parent_node_id",
                        column: x => x.parent_node_id,
                        principalTable: "determination_graph_nodes",
                        principalColumn: "node_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "determination_graphs",
                columns: table => new
                {
                    determination_graph_id = table.Column<string>(nullable: false),
                    start_node_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_determination_graphs", x => x.determination_graph_id);
                    table.ForeignKey(
                        name: "FK_determination_graphs_determination_graph_nodes_start_node_id",
                        column: x => x.start_node_id,
                        principalTable: "determination_graph_nodes",
                        principalColumn: "node_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "roadwork_schemas",
                columns: table => new
                {
                    schema_id = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    image_id = table.Column<string>(nullable: true),
                    category = table.Column<byte>(nullable: false, defaultValue: (byte)0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roadwork_schemas", x => x.schema_id);
                    table.ForeignKey(
                        name: "FK_roadwork_schemas_images_image_id",
                        column: x => x.image_id,
                        principalTable: "images",
                        principalColumn: "image_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "city_report_group",
                columns: table => new
                {
                    CityId = table.Column<string>(nullable: false),
                    ReportGroupId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_city_report_group", x => new { x.CityId, x.ReportGroupId });
                    table.ForeignKey(
                        name: "FK_city_report_group_cities_CityId",
                        column: x => x.CityId,
                        principalTable: "cities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_city_report_group_reportgroups_ReportGroupId",
                        column: x => x.ReportGroupId,
                        principalTable: "reportgroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "email_report_group",
                columns: table => new
                {
                    EmailId = table.Column<string>(nullable: false),
                    ReportGroupId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_email_report_group", x => new { x.EmailId, x.ReportGroupId });
                    table.ForeignKey(
                        name: "FK_email_report_group_emails_reportgroups_EmailId",
                        column: x => x.EmailId,
                        principalTable: "emails_reportgroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_email_report_group_reportgroups_ReportGroupId",
                        column: x => x.ReportGroupId,
                        principalTable: "reportgroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_report_group",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    ReportGroupId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_report_group", x => new { x.UserId, x.ReportGroupId });
                    table.ForeignKey(
                        name: "FK_user_report_group_reportgroups_ReportGroupId",
                        column: x => x.ReportGroupId,
                        principalTable: "reportgroups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_report_group_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "report_images",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    report_id = table.Column<string>(nullable: true),
                    image_id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_report_images", x => x.id);
                    table.ForeignKey(
                        name: "FK_report_images_images_image_id",
                        column: x => x.image_id,
                        principalTable: "images",
                        principalColumn: "image_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_report_images_reports_report_id",
                        column: x => x.report_id,
                        principalTable: "reports",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "bboxes",
                columns: table => new
                {
                    bbox_id = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: true),
                    schema_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bboxes", x => x.bbox_id);
                    table.ForeignKey(
                        name: "FK_bboxes_roadwork_schemas_schema_id",
                        column: x => x.schema_id,
                        principalTable: "roadwork_schemas",
                        principalColumn: "schema_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "bbox_points",
                columns: table => new
                {
                    point_id = table.Column<string>(nullable: false),
                    x = table.Column<double>(nullable: false),
                    y = table.Column<double>(nullable: false),
                    bbox_id = table.Column<string>(nullable: true),
                    order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bbox_points", x => x.point_id);
                    table.ForeignKey(
                        name: "FK_bbox_points_bboxes_bbox_id",
                        column: x => x.bbox_id,
                        principalTable: "bboxes",
                        principalColumn: "bbox_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "cities",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { "2735068b-489a-4d38-b230-fd5ec64927ad", "Alken" },
                    { "1fe2d341-cae5-45a5-83ef-95973181d9c9", "Kortessem" },
                    { "68331754-2d45-462a-9b81-dde18171b2e4", "Lanaken" },
                    { "4f7ad1e6-4f2b-4ef1-8612-1e8c8130150e", "Leopoldsburg" },
                    { "2febf7ad-d17f-4cd7-bb05-178320fe71e0", "Lommel" },
                    { "f5e56030-6f4c-47e6-b9d0-3a0158e907c9", "Lummen" },
                    { "c4e1ce21-511d-4899-a272-b42a86bc83f3", "Maaseik" },
                    { "5f1d8cf7-f4a3-4fc9-b528-c7c2e2df02d6", "Maasmechelen" },
                    { "0d5d3a55-683f-4df1-90c9-2ee5631969ca", "Nieuwerkerken" },
                    { "16aa0467-578b-47f1-9767-02649ffe5b1d", "Oudsbergen" },
                    { "cf61cd1b-e046-4a67-a672-e6b3d38b184b", "Peer" },
                    { "beb93851-316c-4c57-8e48-6e266b3cf4d4", "Pelt" },
                    { "94f89cd9-d7d5-413f-a7be-0a7b6206558d", "Riemst" },
                    { "54ac6b50-a8a9-4a47-8525-aa843780a250", "Sint-Truiden" },
                    { "476b4e4d-7f85-4ae1-932c-e5fa91d3c12d", "Tessenderlo" },
                    { "fa08a293-12b3-4ca5-ba8b-974069b0e49c", "Tongeren" },
                    { "3c03d2d6-12c2-424e-a2a8-7b67fb01ddcc", "Voeren" },
                    { "01f2e731-27e0-4227-a9a2-18f3d4cc5d43", "Wellen" },
                    { "b3ab31f0-e8c2-482d-8e1e-5dfe12d106b8", "Kinrooi" },
                    { "8e25a60a-a2a4-4b09-9704-8c4696dfddf7", "Houthalen-Helchteren" },
                    { "625ec8f0-819a-4c0d-8d37-723c1925d3ca", "Hoeselt" },
                    { "5c127af3-a149-4a49-8266-bcbf384328e7", "Heusden-Zolder" },
                    { "99c0ac0f-7be5-4c60-bd57-e0eecd59b452", "As" },
                    { "c1244480-a7e3-4af9-9ab6-299d3afc0263", "Beringen" },
                    { "53b25c5b-910e-4d30-9815-e5968e9c3deb", "Bilzen" },
                    { "faada534-9f0b-4828-8f7c-f203567d3361", "Bocholt" },
                    { "dfac2550-6b9d-4a06-bce5-ee1e5461426c", "Borgloon" },
                    { "feaa681a-a73d-4d42-82f3-17c65ad5be72", "Bree" },
                    { "dea9bc46-edd6-4109-8c52-7e197916a813", "Diepenbeek" },
                    { "35e06a67-a5b1-4b94-a0a9-3fbe3a9bf6ec", "Dilsen-Stokkem" },
                    { "6b7cf017-96f1-4f35-b604-48f01612c4dd", "Zonhoven" },
                    { "8510c90e-ce28-4050-8bab-273bd9de969d", "Genk" },
                    { "ea247f6f-49b9-4ad2-bdfa-0b309f6f46d7", "Halen" },
                    { "2749a7c6-a712-4363-a7ab-3c9d9edc1caf", "Ham" },
                    { "f220fe5d-a3b7-4d09-96b4-596ed3bac4fd", "Hamont-Achel" },
                    { "eb74dab7-b30c-432b-bee8-f425db1bb38d", "Hasselt" },
                    { "2ebdb9d1-462f-4ea6-85b9-7f1f1a07271f", "Hechelt-Eksel" },
                    { "a143fcdf-89e7-4800-8e7c-82c8f29d9278", "Heers" },
                    { "ac83c963-e9dc-4e20-9e6a-d57aa0311c7f", "Herk-de-Stad" },
                    { "93a36e78-24eb-436e-980f-45f73e7e2f68", "Herstappe" },
                    { "aa189d35-8208-453d-9bbc-f13051697088", "Gingelom" },
                    { "068868c5-24cf-4653-b17e-ed4cf9f37911", "Zutendaal" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_bbox_points_bbox_id",
                table: "bbox_points",
                column: "bbox_id");

            migrationBuilder.CreateIndex(
                name: "IX_bboxes_schema_id",
                table: "bboxes",
                column: "schema_id");

            migrationBuilder.CreateIndex(
                name: "IX_city_report_group_ReportGroupId",
                table: "city_report_group",
                column: "ReportGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_determination_graph_answers_node_id",
                table: "determination_graph_answers",
                column: "node_id",
                unique: true,
                filter: "[node_id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_determination_graph_answers_parent_node_id",
                table: "determination_graph_answers",
                column: "parent_node_id");

            migrationBuilder.CreateIndex(
                name: "IX_determination_graphs_start_node_id",
                table: "determination_graphs",
                column: "start_node_id",
                unique: true,
                filter: "[start_node_id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_email_report_group_ReportGroupId",
                table: "email_report_group",
                column: "ReportGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_login_records_user_id",
                table: "login_records",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_refresh_tokens_user_id",
                table: "refresh_tokens",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_report_images_image_id",
                table: "report_images",
                column: "image_id");

            migrationBuilder.CreateIndex(
                name: "IX_report_images_report_id",
                table: "report_images",
                column: "report_id");

            migrationBuilder.CreateIndex(
                name: "IX_roadwork_schemas_image_id",
                table: "roadwork_schemas",
                column: "image_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_report_group_ReportGroupId",
                table: "user_report_group",
                column: "ReportGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "bbox_points");

            migrationBuilder.DropTable(
                name: "blacklist_emails");

            migrationBuilder.DropTable(
                name: "city_report_group");

            migrationBuilder.DropTable(
                name: "determination_graph_answers");

            migrationBuilder.DropTable(
                name: "determination_graphs");

            migrationBuilder.DropTable(
                name: "email_report_group");

            migrationBuilder.DropTable(
                name: "login_records");

            migrationBuilder.DropTable(
                name: "priority_emails");

            migrationBuilder.DropTable(
                name: "refresh_tokens");

            migrationBuilder.DropTable(
                name: "report_default_issues");

            migrationBuilder.DropTable(
                name: "report_images");

            migrationBuilder.DropTable(
                name: "user_report_group");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "bboxes");

            migrationBuilder.DropTable(
                name: "cities");

            migrationBuilder.DropTable(
                name: "determination_graph_nodes");

            migrationBuilder.DropTable(
                name: "emails_reportgroups");

            migrationBuilder.DropTable(
                name: "reports");

            migrationBuilder.DropTable(
                name: "reportgroups");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "roadwork_schemas");

            migrationBuilder.DropTable(
                name: "images");
        }
    }
}
