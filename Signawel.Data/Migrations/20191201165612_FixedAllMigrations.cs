using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Signawel.Data.Migrations
{
    public partial class FixedAllMigrations : Migration
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
                name: "categories",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    order_id = table.Column<int>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    image_path = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.id);
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
                    image_id = table.Column<string>(nullable: true)
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
                name: "reports",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    sender_email = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: false),
                    roadwork_id = table.Column<string>(nullable: false),
                    IssueId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reports", x => x.id);
                    table.ForeignKey(
                        name: "FK_reports_report_default_issues_IssueId",
                        column: x => x.IssueId,
                        principalTable: "report_default_issues",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "report_images",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    report_id = table.Column<string>(nullable: true),
                    image_path = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_report_images", x => x.id);
                    table.ForeignKey(
                        name: "FK_report_images_reports_report_id",
                        column: x => x.report_id,
                        principalTable: "reports",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
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
                    { "2b4a2266-49f5-4d41-b09b-afbc7798d610", "Alken" },
                    { "175aa7db-ce54-4987-9d1e-19b279cd570a", "Kortessem" },
                    { "983ebb16-c448-414e-a2f4-5673fc67849f", "Lanaken" },
                    { "cebed561-b1c6-41ed-b792-0d61c43621f0", "Leopoldsburg" },
                    { "efdc5db8-7af3-4c19-b30e-791bcf4c0be2", "Lommel" },
                    { "f33335e8-5fd6-4c88-aec9-6038a359513d", "Lummen" },
                    { "64371ce6-00d9-4e0b-b798-5e733b1c9153", "Maaseik" },
                    { "42d0da53-c1b8-422b-a0cf-c73f79487e25", "Maasmechelen" },
                    { "9b07fa13-f3d3-4359-94a8-bdccf0d185c7", "Nieuwerkerken" },
                    { "0a7e443a-2b55-440e-a218-318a6224858d", "Oudsbergen" },
                    { "c4ef8b5f-a7ae-4f3a-b771-40f728caf2d0", "Peer" },
                    { "6aaf3137-6ffb-49e4-86f5-fe5d1caa4337", "Pelt" },
                    { "8839c271-7a9c-47a3-90a0-d3051f4b0cab", "Riemst" },
                    { "d8c192b2-79d1-4fcf-a299-4479ffc803e0", "Sint-Truiden" },
                    { "55e4a13b-be73-493c-83dd-9906a10cf768", "Tessenderlo" },
                    { "b65276cf-a159-451f-9ed3-866202a191b5", "Tongeren" },
                    { "5a84a2e4-6bbf-4e6e-8d2a-8fc4141f14f9", "Voeren" },
                    { "ea6f2104-bc20-4d12-93f3-68c04cb150c5", "Wellen" },
                    { "fa6ffe60-d49b-4cb5-bfef-9a9984cf4047", "Kinrooi" },
                    { "e3704ae2-9c8e-4744-abf5-995ad87def3e", "Houthalen-Helchteren" },
                    { "7487d9ef-ad73-44ac-a28a-4200235c6a18", "Hoeselt" },
                    { "0a5f419b-e546-4d64-8a69-f1976d7fd4ad", "Heusden-Zolder" },
                    { "c386e621-9f38-43a1-b7c4-ac13efb7fc8e", "As" },
                    { "ef4846ba-3260-43b4-9fd2-dce297090261", "Beringen" },
                    { "c0670dea-2db6-4769-89ae-b21b4a7b901c", "Bilzen" },
                    { "40f37b91-bd21-4c52-afc5-dd457c7d09f8", "Bocholt" },
                    { "b837250a-423d-4cf7-bb36-32ad7cfed4c9", "Borgloon" },
                    { "b643e362-2d3e-4c18-b5b7-460fe6b25068", "Bree" },
                    { "efa88d13-b487-4936-8907-5546bf2ac7a7", "Diepenbeek" },
                    { "2667048c-5659-4832-9147-4c1436d8d521", "Dilsen-Stokkem" },
                    { "ceba1175-8619-43f4-b8cf-2b41e727bcde", "Zonhoven" },
                    { "be0e635a-ecf3-4280-8c0f-5918079642a5", "Genk" },
                    { "b4f58173-8fed-44c2-a912-d114ca879c12", "Halen" },
                    { "d1f8b474-3fd1-4946-8576-19901c94ec95", "Ham" },
                    { "6a25fece-dec7-47b9-9ec6-d4ef638fbc8a", "Hamont-Achel" },
                    { "ee0d5564-f65a-46ec-b130-beee14634450", "Hasselt" },
                    { "72fd4f16-a6f7-4582-bc78-7b10eac5fcf8", "Hechelt-Eksel" },
                    { "3cec904d-6fb5-4acc-9ed6-a64b992bfb04", "Heers" },
                    { "1bad5687-be6d-4aa2-8090-dbbd82c1885b", "Herk-de-Stad" },
                    { "11111887-7c0b-4914-b2f0-f55430664d75", "Herstappe" },
                    { "76ddd8e9-9c32-40ef-bd29-07fef49a5c8c", "Gingelom" },
                    { "1f5eea52-dd26-46b3-b870-040c51173177", "Zutendaal" }
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
                name: "IX_report_images_report_id",
                table: "report_images",
                column: "report_id");

            migrationBuilder.CreateIndex(
                name: "IX_reports_IssueId",
                table: "reports",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_roadwork_schemas_image_id",
                table: "roadwork_schemas",
                column: "image_id");
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
                name: "categories");

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
                name: "report_images");

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
                name: "reportgroups");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "reports");

            migrationBuilder.DropTable(
                name: "roadwork_schemas");

            migrationBuilder.DropTable(
                name: "report_default_issues");

            migrationBuilder.DropTable(
                name: "images");
        }
    }
}
