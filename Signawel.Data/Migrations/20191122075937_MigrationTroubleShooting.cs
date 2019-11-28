using Microsoft.EntityFrameworkCore.Migrations;

namespace Signawel.Data.Migrations
{
    public partial class MigrationTroubleShooting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_city_report_group_ReportGroupId",
                table: "city_report_group",
                column: "ReportGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_email_report_group_ReportGroupId",
                table: "email_report_group",
                column: "ReportGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "city_report_group");

            migrationBuilder.DropTable(
                name: "email_report_group");

            migrationBuilder.DropTable(
                name: "cities");

            migrationBuilder.DropTable(
                name: "emails_reportgroups");

            migrationBuilder.DropTable(
                name: "reportgroups");
        }
    }
}
