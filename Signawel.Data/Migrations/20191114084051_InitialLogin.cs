using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Signawel.Data.Migrations
{
    public partial class InitialLogin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_refresh_tokens_AspNetUsers_user_id",
                table: "refresh_tokens");

            migrationBuilder.AlterColumn<string>(
                name: "user_id",
                table: "refresh_tokens",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "token",
                table: "refresh_tokens",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "jwt_id",
                table: "refresh_tokens",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "refresh_tokens",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Invalidated",
                table: "refresh_tokens",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Used",
                table: "refresh_tokens",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_refresh_tokens_AspNetUsers_user_id",
                table: "refresh_tokens",
                column: "user_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_refresh_tokens_AspNetUsers_user_id",
                table: "refresh_tokens");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "refresh_tokens");

            migrationBuilder.DropColumn(
                name: "Invalidated",
                table: "refresh_tokens");

            migrationBuilder.DropColumn(
                name: "Used",
                table: "refresh_tokens");

            migrationBuilder.AlterColumn<string>(
                name: "user_id",
                table: "refresh_tokens",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "token",
                table: "refresh_tokens",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "jwt_id",
                table: "refresh_tokens",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_refresh_tokens_AspNetUsers_user_id",
                table: "refresh_tokens",
                column: "user_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
