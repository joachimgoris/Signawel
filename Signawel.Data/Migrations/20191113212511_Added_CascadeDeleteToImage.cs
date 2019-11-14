using Microsoft.EntityFrameworkCore.Migrations;

namespace Signawel.Data.Migrations
{
    public partial class Added_CascadeDeleteToImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_roadwork_schemas_images_image_id",
                table: "roadwork_schemas");

            migrationBuilder.AddForeignKey(
                name: "FK_roadwork_schemas_images_image_id",
                table: "roadwork_schemas",
                column: "image_id",
                principalTable: "images",
                principalColumn: "image_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_roadwork_schemas_images_image_id",
                table: "roadwork_schemas");

            migrationBuilder.AddForeignKey(
                name: "FK_roadwork_schemas_images_image_id",
                table: "roadwork_schemas",
                column: "image_id",
                principalTable: "images",
                principalColumn: "image_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
