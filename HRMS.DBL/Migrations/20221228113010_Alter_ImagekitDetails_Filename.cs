using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    public partial class Alter_ImagekitDetails_Filename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "ImagekitDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.DropColumn(
               name: "ImageKit_Filleid",
               table: "EmployeeDocuments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "ImagekitDetails");

            migrationBuilder.AddColumn<string>(
                name: "ImageKit_Filleid",
                table: "EmployeeDocuments",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
