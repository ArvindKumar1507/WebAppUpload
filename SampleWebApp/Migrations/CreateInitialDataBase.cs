using Microsoft.EntityFrameworkCore.Migrations;

namespace SampleWebApp.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileDetails",
                columns: table => new
                {
                    FileID = table.Column<string>(nullable: false),
                    FileName = table.Column<string>(nullable: true),
                    FileBytes = table.Column<string>(nullable: true),
                    FileType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileDetails", x => x.FileID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileDetails");
        }
    }
}
