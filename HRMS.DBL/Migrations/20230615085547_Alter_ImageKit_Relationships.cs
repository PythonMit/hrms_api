using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    /// <inheritdoc />
    public partial class AlterImageKitRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagekitDetailFileId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ImagekitDetailFileId",
                table: "EmployeeDocuments");

            migrationBuilder.DropColumn(
                name: "ImagekitDetailFileId",
                table: "EmployeeContracts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagekitDetailFileId",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagekitDetailFileId",
                table: "EmployeeDocuments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagekitDetailFileId",
                table: "EmployeeContracts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
