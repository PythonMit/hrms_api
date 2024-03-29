using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    public partial class Alter_EmployeeContracts_DeleteColumnName_DropReason : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DropReason",
                table: "EmployeeContracts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DropReason",
                table: "EmployeeContracts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
