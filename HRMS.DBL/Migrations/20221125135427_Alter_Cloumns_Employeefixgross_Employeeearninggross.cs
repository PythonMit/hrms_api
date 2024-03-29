using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    public partial class Alter_Cloumns_Employeefixgross_Employeeearninggross : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsIncentive",
                table: "EmployeeFixGross",
                newName: "IsFixIncentive");

            migrationBuilder.RenameColumn(
                name: "IncentiveRemarks",
                table: "EmployeeFixGross",
                newName: "FixIncentiveRemarks");

            migrationBuilder.RenameColumn(
                name: "IncentiveDuration",
                table: "EmployeeFixGross",
                newName: "FixIncentiveDuration");

            migrationBuilder.RenameColumn(
                name: "Incentive",
                table: "EmployeeEarningGross",
                newName: "FixIncentive");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsFixIncentive",
                table: "EmployeeFixGross",
                newName: "IsIncentive");

            migrationBuilder.RenameColumn(
                name: "FixIncentiveRemarks",
                table: "EmployeeFixGross",
                newName: "IncentiveRemarks");

            migrationBuilder.RenameColumn(
                name: "FixIncentiveDuration",
                table: "EmployeeFixGross",
                newName: "IncentiveDuration");

            migrationBuilder.RenameColumn(
                name: "FixIncentive",
                table: "EmployeeEarningGross",
                newName: "Incentive");
        }
    }
}
