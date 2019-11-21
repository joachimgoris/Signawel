using Microsoft.EntityFrameworkCore.Migrations;

namespace Signawel.Data.Migrations
{
    public partial class editedReport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "priority",
                table: "reports");

            migrationBuilder.AddColumn<string>(
                name: "cities",
                table: "reports",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "reports",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "roadwork_id",
                table: "reports",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cities",
                table: "reports");

            migrationBuilder.DropColumn(
                name: "description",
                table: "reports");

            migrationBuilder.DropColumn(
                name: "roadwork_id",
                table: "reports");

            migrationBuilder.AddColumn<bool>(
                name: "priority",
                table: "reports",
                nullable: false,
                defaultValue: false);
        }
    }
}
