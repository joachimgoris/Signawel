using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Signawel.Data.Migrations
{
    public partial class FixedAllMigrationProblems : Migration
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
                name: "default_issues",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_default_issues", x => x.id);
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
                    user_email = table.Column<string>(nullable: false),
                    custom_message = table.Column<string>(nullable: true),
                    roadwork_id = table.Column<string>(nullable: true),
                    cities = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true)
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
                name: "report_issues",
                columns: table => new
                {
                    ReportId = table.Column<string>(nullable: false),
                    IssueId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_report_issues", x => new { x.ReportId, x.IssueId });
                    table.ForeignKey(
                        name: "FK_report_issues_default_issues_IssueId",
                        column: x => x.IssueId,
                        principalTable: "default_issues",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_report_issues_reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "reports",
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
                    { "8a633419-800e-4a89-a961-ffca6b345674", "Alken" },
                    { "d9df5550-2d74-4ac9-a2b7-8afceb0217c5", "Kortessem" },
                    { "47393c8a-5e56-4040-b79d-671daff47dc4", "Lanaken" },
                    { "ac330e5e-92d6-4a94-a018-95df1ba1fb16", "Leopoldsburg" },
                    { "6f333ad3-fe7f-429e-b085-6092a499f92a", "Lommel" },
                    { "937253ed-3ff4-4b44-8a56-a262a27720da", "Lummen" },
                    { "9042fccf-1ee5-4341-9654-f1ac90c81721", "Maaseik" },
                    { "7f3db7c7-7cfb-4f84-bd97-4f6240110607", "Maasmechelen" },
                    { "c3e3c6b8-e702-4e0e-82d1-c4bd66f10888", "Nieuwerkerken" },
                    { "f292ee1d-545d-4349-8f76-e87cc51b12eb", "Oudsbergen" },
                    { "ba4a158e-b3c3-4f17-b8d8-ee7954110eb0", "Peer" },
                    { "c0dd9178-c926-4c3e-9ef6-55afa677cec0", "Pelt" },
                    { "27f067bf-0d16-442a-ae65-7eace45d2756", "Riemst" },
                    { "8cf938dc-e948-4b0a-ab15-d380f8bd3b3e", "Sint-Truiden" },
                    { "03405f9e-21a1-4d32-a674-9c49f9c25cb8", "Tessenderlo" },
                    { "95614355-c6cb-4ba2-bc53-f79bca66d6f7", "Tongeren" },
                    { "e2ebab08-9fa4-44d3-af65-4248ac313bae", "Voeren" },
                    { "f26c139b-2ce9-4ddb-938c-43bcae8c65b3", "Wellen" },
                    { "5339b928-01f4-4338-bc79-4c02fdc26bdb", "Kinrooi" },
                    { "2d427602-340c-4aff-b142-bae57b6056d7", "Houthalen-Helchteren" },
                    { "26533f8e-c0e6-4ee7-95bf-ea3c23e6b28d", "Hoeselt" },
                    { "b11c9dc6-b4df-4943-b374-3bc60483f4a4", "Heusden-Zolder" },
                    { "d55f82c4-fc9c-4b4e-88fd-492435720714", "As" },
                    { "53f64ddf-1033-4605-8da6-891ed22482f8", "Beringen" },
                    { "a84979fb-1bda-4ead-90e0-85b97d26c6af", "Bilzen" },
                    { "fff05f3c-cba1-4a62-a85d-2333284ed993", "Bocholt" },
                    { "67e0b43b-473e-491e-b3e7-15df1ec7ae3c", "Borgloon" },
                    { "6577c629-cc7b-491b-b6c6-f4d7c023ab1c", "Bree" },
                    { "006a8dc0-fc62-494d-873e-1bd9c1a1d6e7", "Diepenbeek" },
                    { "8a101db6-0374-4735-9a50-35efab98b28d", "Dilsen-Stokkem" },
                    { "2531b6c9-a173-4105-92b3-ba51a1b4ac52", "Zonhoven" },
                    { "a876ad88-5177-4792-8928-f2e34291b758", "Genk" },
                    { "51966a26-0abd-4380-b895-175a51bba1cc", "Halen" },
                    { "e996e794-bcaa-4ebc-ba4e-b7e31ccff685", "Ham" },
                    { "7b1281e7-5b37-47a3-a518-e11e55b99d97", "Hamont-Achel" },
                    { "b9050566-019c-4b9f-80f3-1116f78048c7", "Hasselt" },
                    { "a15341cd-8d96-41d4-8dd7-9a3e83e9f680", "Hechelt-Eksel" },
                    { "2dd0adf4-2a8e-476f-b34b-0f953519a4f9", "Heers" },
                    { "0df34ea2-13af-4da8-adf6-97a88928f218", "Herk-de-Stad" },
                    { "654c508c-732a-460c-bb7e-4fab63923063", "Herstappe" },
                    { "86a933cc-f513-42ca-916a-54896382cf6c", "Gingelom" },
                    { "e6e04d27-b61b-40ea-8f3f-9905b969a021", "Zutendaal" }
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
                name: "IX_report_issues_IssueId",
                table: "report_issues",
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
                name: "report_issues");

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
                name: "default_issues");

            migrationBuilder.DropTable(
                name: "reports");

            migrationBuilder.DropTable(
                name: "roadwork_schemas");

            migrationBuilder.DropTable(
                name: "images");
        }
    }
}
