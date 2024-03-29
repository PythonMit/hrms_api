using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeLeaveApplicationCommentRecordStatusEntityColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTimeUtc",
                table: "EmployeeLeaveApplicationComments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "RecordStatus",
                table: "EmployeeLeaveApplicationComments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDateTimeUtc",
                table: "EmployeeLeaveApplicationComments",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDateTimeUtc",
                table: "EmployeeLeaveApplicationComments");

            migrationBuilder.DropColumn(
                name: "RecordStatus",
                table: "EmployeeLeaveApplicationComments");

            migrationBuilder.DropColumn(
                name: "UpdatedDateTimeUtc",
                table: "EmployeeLeaveApplicationComments");
        }
    }
}
