using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeLeaveApplicationColumnApprovedDaysLWPDays : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ApprovedDays",
                table: "EmployeeLeaveApplications",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "LWPDays",
                table: "EmployeeLeaveApplications",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedDays",
                table: "EmployeeLeaveApplications");

            migrationBuilder.DropColumn(
                name: "LWPDays",
                table: "EmployeeLeaveApplications");
        }
    }
}
