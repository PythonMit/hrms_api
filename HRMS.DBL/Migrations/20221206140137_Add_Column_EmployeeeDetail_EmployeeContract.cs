using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    public partial class Add_Column_EmployeeeDetail_EmployeeContract : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InstituteName",
                table: "EmployeeDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DropRemarks",
                table: "EmployeeContracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsProjectTrainee",
                table: "EmployeeContracts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "NoticeRemarks",
                table: "EmployeeContracts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "EmployeeContracts",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InstituteName",
                table: "EmployeeDetails");

            migrationBuilder.DropColumn(
                name: "DropRemarks",
                table: "EmployeeContracts");

            migrationBuilder.DropColumn(
                name: "IsProjectTrainee",
                table: "EmployeeContracts");

            migrationBuilder.DropColumn(
                name: "NoticeRemarks",
                table: "EmployeeContracts");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "EmployeeContracts");
        }
    }
}
