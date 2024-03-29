using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    public partial class Alter_Employees : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagekitDetailFileId",
                table: "Employees",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ImagekitDetailFileId",
                table: "Employees",
                column: "ImagekitDetailFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_ImagekitDetails_ImagekitDetailFileId",
                table: "Employees",
                column: "ImagekitDetailFileId",
                principalTable: "ImagekitDetails",
                principalColumn: "FileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_ImagekitDetails_ImagekitDetailFileId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ImagekitDetailFileId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ImagekitDetailFileId",
                table: "Employees");
        }
    }
}
