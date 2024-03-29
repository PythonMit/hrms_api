using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    /// <inheritdoc />
    public partial class AlterEmployeeLeaveApplicationsAddApprovedFromDateApprovedToDateDeclineDays : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedFromDate",
                table: "EmployeeLeaveApplications",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedToDate",
                table: "EmployeeLeaveApplications",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "DeclineDays",
                table: "EmployeeLeaveApplications",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedFromDate",
                table: "EmployeeLeaveApplications");

            migrationBuilder.DropColumn(
                name: "ApprovedToDate",
                table: "EmployeeLeaveApplications");

            migrationBuilder.DropColumn(
                name: "DeclineDays",
                table: "EmployeeLeaveApplications");
        }
    }
}
