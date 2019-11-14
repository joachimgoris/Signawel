using Microsoft.EntityFrameworkCore.Migrations;

namespace Signawel.Data.Migrations
{
    public partial class Added_MoreCascadingDeletes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bbox_points_bboxes_bbox_id",
                table: "bbox_points");

            migrationBuilder.AddForeignKey(
                name: "FK_bbox_points_bboxes_bbox_id",
                table: "bbox_points",
                column: "bbox_id",
                principalTable: "bboxes",
                principalColumn: "bbox_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bbox_points_bboxes_bbox_id",
                table: "bbox_points");

            migrationBuilder.AddForeignKey(
                name: "FK_bbox_points_bboxes_bbox_id",
                table: "bbox_points",
                column: "bbox_id",
                principalTable: "bboxes",
                principalColumn: "bbox_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
