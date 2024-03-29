using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    /// <inheritdoc />
    public partial class AddValueTerminateUpdateNoticePeriodEmployeeContractStatusType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "EmployeeContractStatus",
                keyColumn: "Id",
                keyValue: 3,
                column: "StatusType",
                value: "Notice Period");

            migrationBuilder.InsertData(
                table: "EmployeeContractStatus",
                columns: new[] { "Id", "CreatedDateTimeUtc", "Description", "RecordStatus", "StatusType", "UpdatedDateTimeUtc" },
                values: new object[] { 5, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Terminate", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmployeeContractStatus",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "EmployeeContractStatus",
                keyColumn: "Id",
                keyValue: 3,
                column: "StatusType",
                value: "NoticePeriod");
        }
    }
}
