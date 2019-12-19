using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Signawel.Data.Migrations
{
    public partial class migrationreset : Migration
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
                    { "a1c5d266-4f21-4e8c-8f54-34cc63464fae", "Alken" },
                    { "760d0d43-09f6-4657-a9ae-15ccc4085a4f", "Kortessem" },
                    { "049b0b27-e8a1-4a18-bed7-5a509829367c", "Lanaken" },
                    { "566d13d6-ac9f-4b67-8e29-0b35b1d1c358", "Leopoldsburg" },
                    { "8f0c65e1-dd03-482e-9896-6d4b69b9a090", "Lommel" },
                    { "ce9d5530-82dc-41cb-ab8a-d1a65a003b03", "Lummen" },
                    { "02f38c68-7378-49fb-acb0-b8dd327a937e", "Maaseik" },
                    { "420ba5d3-a4b8-4f06-982c-941aa9714312", "Maasmechelen" },
                    { "77175dc4-2738-4292-85eb-4d002a1efaa6", "Nieuwerkerken" },
                    { "87226eec-8b9c-412f-8372-0d24d8655ce5", "Oudsbergen" },
                    { "18d48b43-b028-47fb-a285-ae0547d5ce7f", "Peer" },
                    { "b95320ba-c674-42d1-b5f9-40f44ea50bd7", "Pelt" },
                    { "ec4c91b4-19a1-42de-b798-1d838e376c8e", "Riemst" },
                    { "cb46e81e-872f-4cfd-b583-bbfa347d4408", "Sint-Truiden" },
                    { "a16c75c5-1f41-4aa5-9d42-0fc65b2b9b01", "Tessenderlo" },
                    { "20c5c2fd-448c-4d20-a6c1-a700a624eff5", "Tongeren" },
                    { "32139788-73db-415a-8148-3526f2e0f8e1", "Voeren" },
                    { "bdd7e114-bcc8-48b8-8c93-db43cae1d66d", "Wellen" },
                    { "ce229f20-0558-4d5a-aacf-ca113b73fddd", "Kinrooi" },
                    { "ae68cd26-3773-4ac2-9e36-4b2ae9f7f784", "Houthalen-Helchteren" },
                    { "ffa781da-2607-4ba3-a866-7a2f116cf357", "Hoeselt" },
                    { "a8519f51-2d62-4d28-9ba2-080dbe98add4", "Heusden-Zolder" },
                    { "c3ae6f57-5915-4013-9fc7-030e3ffd31c0", "As" },
                    { "8afd1cb8-de3b-4f5a-ba0f-1a4513286754", "Beringen" },
                    { "a803b0e8-e84e-41dd-84b7-bd6e42a2603c", "Bilzen" },
                    { "e1ea5cba-3e09-4aea-bf2b-66db5a42571b", "Bocholt" },
                    { "0e397f19-e638-4c41-a6d7-3e5ca75dcf12", "Borgloon" },
                    { "f40edad9-3b57-457a-81ce-77aaa9046af9", "Bree" },
                    { "fdd394b3-1823-4ec2-879c-8907f3464f80", "Diepenbeek" },
                    { "da0d624d-bf04-4ecf-b1fd-a0f419daa7fd", "Dilsen-Stokkem" },
                    { "91dac0d7-a73c-45c4-a6f5-3ab0b09df448", "Zonhoven" },
                    { "11890129-10a8-4181-8bcc-da9ae74a176e", "Genk" },
                    { "0a42157d-1dd7-450b-a671-c503fd8420b6", "Halen" },
                    { "e6c3aa4f-aff1-470b-a712-19228095b505", "Ham" },
                    { "72b999b4-cfb6-4c8a-be18-c264a2f52800", "Hamont-Achel" },
                    { "848b1b72-6e86-4718-aa88-c6c12de67935", "Hasselt" },
                    { "421f17bf-9ba8-436f-8ae5-afa3949876a5", "Hechelt-Eksel" },
                    { "c10bd937-f45d-44d9-b52f-9e0a37284cbb", "Heers" },
                    { "d660ab8a-cb33-4b24-87aa-a0a235542cc9", "Herk-de-Stad" },
                    { "fb00d460-00a4-4694-9cc5-29dfc03243b0", "Herstappe" },
                    { "39723310-9a1c-4034-ba1a-ba2751c7fa30", "Gingelom" },
                    { "5e7b1e29-c4df-4c6f-972d-c1b3366ec779", "Zutendaal" }
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
                name: "images");
        }
    }
}
