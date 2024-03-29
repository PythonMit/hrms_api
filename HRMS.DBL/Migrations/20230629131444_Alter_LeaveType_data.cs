using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    /// <inheritdoc />
    public partial class AlterLeaveTypedata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "TotalLeaves" },
                values: new object[] { "Carry Forward", 0 });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "TotalLeaves" },
                values: new object[] { "Flat Leave", 15 });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Name", "TotalLeaves" },
                values: new object[] { "Leave Without Pay", 3 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "TotalLeaves" },
                values: new object[] { "Casual Leave", 5 });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Name", "TotalLeaves" },
                values: new object[] { "Privilege Leave", 8 });

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Name", "TotalLeaves" },
                values: new object[] { "Sick Leave", 5 });

            migrationBuilder.InsertData(
                table: "LeaveTypes",
                columns: new[] { "Id", "CreatedDateTimeUtc", "Description", "Name", "RecordStatus", "TotalLeaves", "UpdatedDateTimeUtc" },
                values: new object[] { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Leave Without Pay", 1, 3, null });
        }
    }
}
