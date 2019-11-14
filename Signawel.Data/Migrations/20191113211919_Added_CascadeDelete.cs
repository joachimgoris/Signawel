using Microsoft.EntityFrameworkCore.Migrations;

namespace Signawel.Data.Migrations
{
    public partial class Added_CascadeDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bboxes_roadwork_schemas_schema_id",
                table: "bboxes");

            migrationBuilder.AddForeignKey(
                name: "FK_bboxes_roadwork_schemas_schema_id",
                table: "bboxes",
                column: "schema_id",
                principalTable: "roadwork_schemas",
                principalColumn: "schema_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bboxes_roadwork_schemas_schema_id",
                table: "bboxes");

            migrationBuilder.AddForeignKey(
                name: "FK_bboxes_roadwork_schemas_schema_id",
                table: "bboxes",
                column: "schema_id",
                principalTable: "roadwork_schemas",
                principalColumn: "schema_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
