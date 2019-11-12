using Microsoft.EntityFrameworkCore.Migrations;

namespace Signawel.Data.Migrations
{
    public partial class Determination : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "determination_graph_nodes",
                columns: table => new
                {
                    node_id = table.Column<string>(nullable: false),
                    type = table.Column<byte>(nullable: false),
                    question = table.Column<string>(nullable: true),
                    schema_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_determination_graph_nodes", x => x.node_id);
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
                        onDelete: ReferentialAction.Restrict);
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
                        onDelete: ReferentialAction.Restrict);
                });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "determination_graph_answers");

            migrationBuilder.DropTable(
                name: "determination_graphs");

            migrationBuilder.DropTable(
                name: "determination_graph_nodes");
        }
    }
}
