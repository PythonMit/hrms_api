using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRMS.DBL.Migrations
{
    /// <inheritdoc />
    public partial class InsertEmployeeIncentiveStatusMasteData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "EmployeeIncentiveStatus",
                newName: "StatusType");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTimeUtc",
                table: "EmployeeIncentiveStatus",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "RecordStatus",
                table: "EmployeeIncentiveStatus",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDateTimeUtc",
                table: "EmployeeIncentiveStatus",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "EmployeeIncentiveStatus",
                columns: new[] { "Id", "CreatedDateTimeUtc", "Description", "RecordStatus", "StatusType", "UpdatedDateTimeUtc" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Pending", null },
                    { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Paid", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmployeeIncentiveStatus",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EmployeeIncentiveStatus",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "CreatedDateTimeUtc",
                table: "EmployeeIncentiveStatus");

            migrationBuilder.DropColumn(
                name: "RecordStatus",
                table: "EmployeeIncentiveStatus");

            migrationBuilder.DropColumn(
                name: "UpdatedDateTimeUtc",
                table: "EmployeeIncentiveStatus");

            migrationBuilder.RenameColumn(
                name: "StatusType",
                table: "EmployeeIncentiveStatus",
                newName: "Name");
        }
    }
}
