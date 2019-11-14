using Microsoft.EntityFrameworkCore.Migrations;

namespace Signawel.Data.Migrations
{
    public partial class Added_DescriptionToDeterminationQuestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_determination_graph_answers_determination_graph_nodes_node_id",
                table: "determination_graph_answers");

            migrationBuilder.DropForeignKey(
                name: "FK_determination_graphs_determination_graph_nodes_start_node_id",
                table: "determination_graphs");

            migrationBuilder.AddColumn<string>(
                name: "QuestionDescription",
                table: "determination_graph_nodes",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_determination_graph_answers_determination_graph_nodes_node_id",
                table: "determination_graph_answers",
                column: "node_id",
                principalTable: "determination_graph_nodes",
                principalColumn: "node_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_determination_graphs_determination_graph_nodes_start_node_id",
                table: "determination_graphs",
                column: "start_node_id",
                principalTable: "determination_graph_nodes",
                principalColumn: "node_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_determination_graph_answers_determination_graph_nodes_node_id",
                table: "determination_graph_answers");

            migrationBuilder.DropForeignKey(
                name: "FK_determination_graphs_determination_graph_nodes_start_node_id",
                table: "determination_graphs");

            migrationBuilder.DropColumn(
                name: "QuestionDescription",
                table: "determination_graph_nodes");

            migrationBuilder.AddForeignKey(
                name: "FK_determination_graph_answers_determination_graph_nodes_node_id",
                table: "determination_graph_answers",
                column: "node_id",
                principalTable: "determination_graph_nodes",
                principalColumn: "node_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_determination_graphs_determination_graph_nodes_start_node_id",
                table: "determination_graphs",
                column: "start_node_id",
                principalTable: "determination_graph_nodes",
                principalColumn: "node_id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
