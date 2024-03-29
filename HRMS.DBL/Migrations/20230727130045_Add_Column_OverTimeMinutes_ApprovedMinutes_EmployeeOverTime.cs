using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnOverTimeMinutesApprovedMinutesEmployeeOverTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeSpan>(
                name: "ApprovedMinutes",
                table: "EmployeeOverTimes",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "OverTimeMinutes",
                table: "EmployeeOverTimes",
                type: "time",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedMinutes",
                table: "EmployeeOverTimes");

            migrationBuilder.DropColumn(
                name: "OverTimeMinutes",
                table: "EmployeeOverTimes");
        }
    }
}
