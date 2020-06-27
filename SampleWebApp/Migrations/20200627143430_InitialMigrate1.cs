using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SampleWebApp.Migrations
{
    public partial class InitialMigrate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "FileDetails");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "UserDetails",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "FileDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "FileDetails",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "FileDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedTime",
                table: "FileDetails",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "FileDetails");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "FileDetails");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "FileDetails");

            migrationBuilder.DropColumn(
                name: "ModifiedTime",
                table: "FileDetails");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "FileDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
