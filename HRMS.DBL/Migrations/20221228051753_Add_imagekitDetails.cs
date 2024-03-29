using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    public partial class Add_imagekitDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagekitDetailFileId",
                table: "EmployeeDocuments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ImagekitDetails",
                columns: table => new
                {
                    FileId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsPrivateFile = table.Column<bool>(type: "bit", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Thumbnail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecordStatus = table.Column<int>(type: "int", nullable: false),
                    CreatedDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByUserId = table.Column<int>(type: "int", nullable: true),
                    UpdatedByUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImagekitDetails", x => x.FileId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDocuments_ImagekitDetailFileId",
                table: "EmployeeDocuments",
                column: "ImagekitDetailFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeDocuments_ImagekitDetails_ImagekitDetailFileId",
                table: "EmployeeDocuments",
                column: "ImagekitDetailFileId",
                principalTable: "ImagekitDetails",
                principalColumn: "FileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeDocuments_ImagekitDetails_ImagekitDetailFileId",
                table: "EmployeeDocuments");

            migrationBuilder.DropTable(
                name: "ImagekitDetails");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeDocuments_ImagekitDetailFileId",
                table: "EmployeeDocuments");

            migrationBuilder.DropColumn(
                name: "ImagekitDetailFileId",
                table: "EmployeeDocuments");
        }
    }
}
