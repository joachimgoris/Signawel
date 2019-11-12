using Microsoft.EntityFrameworkCore.Migrations;

namespace Signawel.Data.Migrations
{
    public partial class Add_ImagesAndRoadworkSchemas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "bbox_points",
                columns: table => new
                {
                    point_id = table.Column<string>(nullable: false),
                    x = table.Column<double>(nullable: false),
                    y = table.Column<double>(nullable: false),
                    bbox_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bbox_points", x => x.point_id);
                    table.ForeignKey(
                        name: "FK_bbox_points_bboxes_bbox_id",
                        column: x => x.bbox_id,
                        principalTable: "bboxes",
                        principalColumn: "bbox_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bbox_points_bbox_id",
                table: "bbox_points",
                column: "bbox_id");

            migrationBuilder.CreateIndex(
                name: "IX_bboxes_schema_id",
                table: "bboxes",
                column: "schema_id");

            migrationBuilder.CreateIndex(
                name: "IX_roadwork_schemas_image_id",
                table: "roadwork_schemas",
                column: "image_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bbox_points");

            migrationBuilder.DropTable(
                name: "bboxes");

            migrationBuilder.DropTable(
                name: "roadwork_schemas");

            migrationBuilder.DropTable(
                name: "images");
        }
    }
}
