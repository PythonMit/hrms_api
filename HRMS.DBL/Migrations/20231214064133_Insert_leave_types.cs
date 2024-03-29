using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    /// <inheritdoc />
    public partial class Insertleavetypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "EmployeeLeaveStatus",
                columns: new[] { "Id", "CreatedDateTimeUtc", "Description", "RecordStatus", "StatusType", "UpdatedDateTimeUtc" },
                values: new object[] { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "LWP", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmployeeLeaveStatus",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
