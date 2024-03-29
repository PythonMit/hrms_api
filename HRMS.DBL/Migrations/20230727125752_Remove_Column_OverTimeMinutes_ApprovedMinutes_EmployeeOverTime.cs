using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    /// <inheritdoc />
    public partial class RemoveColumnOverTimeMinutesApprovedMinutesEmployeeOverTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedMinutes",
                table: "EmployeeOverTimes");

            migrationBuilder.DropColumn(
                name: "OverTimeMinutes",
                table: "EmployeeOverTimes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedMinutes",
                table: "EmployeeOverTimes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OverTimeMinutes",
                table: "EmployeeOverTimes",
                type: "datetime2",
                nullable: true);
        }
    }
}
