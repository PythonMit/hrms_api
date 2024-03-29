using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    public partial class Update_MasterTable_RecordStausColumnValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: 1,
                column: "RecordStatus",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: 2,
                column: "RecordStatus",
                value: 1);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "RecordStatus",
                value: 1);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "RecordStatus",
                value: 1);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "RecordStatus",
                value: 1);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "RecordStatus",
                value: 1);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "RecordStatus",
                value: 1);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "RecordStatus",
                value: 1);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 7,
                column: "RecordStatus",
                value: 1);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 8,
                column: "RecordStatus",
                value: 1);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 9,
                column: "RecordStatus",
                value: 1);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 10,
                column: "RecordStatus",
                value: 1);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 11,
                column: "RecordStatus",
                value: 1);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 12,
                column: "RecordStatus",
                value: 1);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 13,
                column: "RecordStatus",
                value: 1);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 14,
                column: "RecordStatus",
                value: 1);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 15,
                column: "RecordStatus",
                value: 1);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 16,
                column: "RecordStatus",
                value: 1);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 17,
                column: "RecordStatus",
                value: 1);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 18,
                column: "RecordStatus",
                value: 1);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 19,
                column: "RecordStatus",
                value: 1);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 20,
                column: "RecordStatus",
                value: 1);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "RecordStatus",
                value: 1);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "RecordStatus",
                value: 1);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "RecordStatus",
                value: 1);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "RecordStatus",
                value: 1);

            migrationBuilder.UpdateData(
                table: "EmployeeContractStatus",
                keyColumn: "Id",
                keyValue: 1,
                column: "RecordStatus",
                value: 1);

            migrationBuilder.UpdateData(
                table: "EmployeeContractStatus",
                keyColumn: "Id",
                keyValue: 2,
                column: "RecordStatus",
                value: 1);

            migrationBuilder.UpdateData(
                table: "EmployeeContractStatus",
                keyColumn: "Id",
                keyValue: 3,
                column: "RecordStatus",
                value: 1);

            migrationBuilder.UpdateData(
                table: "EmployeeContractStatus",
                keyColumn: "Id",
                keyValue: 4,
                column: "RecordStatus",
                value: 1);

            migrationBuilder.UpdateData(
                table: "EmployeeEarningGrossStatus",
                keyColumn: "Id",
                keyValue: 1,
                column: "RecordStatus",
                value: 1);

            migrationBuilder.UpdateData(
                table: "EmployeeEarningGrossStatus",
                keyColumn: "Id",
                keyValue: 2,
                column: "RecordStatus",
                value: 1);

            migrationBuilder.InsertData(
                table: "EmployeeEarningGrossStatus",
                columns: new[] { "Id", "CreatedDateTimeUtc", "Description", "RecordStatus", "StatusType", "UpdatedDateTimeUtc" },
                values: new object[] { 3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Paid", null });

            migrationBuilder.UpdateData(
                table: "EmployeeOverTimeStatus",
                keyColumn: "Id",
                keyValue: 1,
                column: "RecordStatus",
                value: 1);

            migrationBuilder.UpdateData(
                table: "EmployeeOverTimeStatus",
                keyColumn: "Id",
                keyValue: 2,
                column: "RecordStatus",
                value: 1);

            migrationBuilder.UpdateData(
                table: "EmployeeOverTimeStatus",
                keyColumn: "Id",
                keyValue: 3,
                column: "RecordStatus",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "RecordStatus",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "RecordStatus",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "RecordStatus",
                value: 1);

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "RecordStatus",
                value: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmployeeEarningGrossStatus",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: 1,
                column: "RecordStatus",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Branches",
                keyColumn: "Id",
                keyValue: 2,
                column: "RecordStatus",
                value: 0);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "RecordStatus",
                value: 0);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "RecordStatus",
                value: 0);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "RecordStatus",
                value: 0);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "RecordStatus",
                value: 0);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 5,
                column: "RecordStatus",
                value: 0);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 6,
                column: "RecordStatus",
                value: 0);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 7,
                column: "RecordStatus",
                value: 0);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 8,
                column: "RecordStatus",
                value: 0);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 9,
                column: "RecordStatus",
                value: 0);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 10,
                column: "RecordStatus",
                value: 0);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 11,
                column: "RecordStatus",
                value: 0);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 12,
                column: "RecordStatus",
                value: 0);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 13,
                column: "RecordStatus",
                value: 0);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 14,
                column: "RecordStatus",
                value: 0);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 15,
                column: "RecordStatus",
                value: 0);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 16,
                column: "RecordStatus",
                value: 0);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 17,
                column: "RecordStatus",
                value: 0);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 18,
                column: "RecordStatus",
                value: 0);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 19,
                column: "RecordStatus",
                value: 0);

            migrationBuilder.UpdateData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 20,
                column: "RecordStatus",
                value: 0);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "RecordStatus",
                value: 0);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "RecordStatus",
                value: 0);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "RecordStatus",
                value: 0);

            migrationBuilder.UpdateData(
                table: "DocumentTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "RecordStatus",
                value: 0);

            migrationBuilder.UpdateData(
                table: "EmployeeContractStatus",
                keyColumn: "Id",
                keyValue: 1,
                column: "RecordStatus",
                value: 0);

            migrationBuilder.UpdateData(
                table: "EmployeeContractStatus",
                keyColumn: "Id",
                keyValue: 2,
                column: "RecordStatus",
                value: 0);

            migrationBuilder.UpdateData(
                table: "EmployeeContractStatus",
                keyColumn: "Id",
                keyValue: 3,
                column: "RecordStatus",
                value: 0);

            migrationBuilder.UpdateData(
                table: "EmployeeContractStatus",
                keyColumn: "Id",
                keyValue: 4,
                column: "RecordStatus",
                value: 0);

            migrationBuilder.UpdateData(
                table: "EmployeeEarningGrossStatus",
                keyColumn: "Id",
                keyValue: 1,
                column: "RecordStatus",
                value: 0);

            migrationBuilder.UpdateData(
                table: "EmployeeEarningGrossStatus",
                keyColumn: "Id",
                keyValue: 2,
                column: "RecordStatus",
                value: 0);

            migrationBuilder.UpdateData(
                table: "EmployeeOverTimeStatus",
                keyColumn: "Id",
                keyValue: 1,
                column: "RecordStatus",
                value: 0);

            migrationBuilder.UpdateData(
                table: "EmployeeOverTimeStatus",
                keyColumn: "Id",
                keyValue: 2,
                column: "RecordStatus",
                value: 0);

            migrationBuilder.UpdateData(
                table: "EmployeeOverTimeStatus",
                keyColumn: "Id",
                keyValue: 3,
                column: "RecordStatus",
                value: 0);

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "RecordStatus",
                value: 0);

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "RecordStatus",
                value: 0);

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "RecordStatus",
                value: 0);

            migrationBuilder.UpdateData(
                table: "LeaveTypes",
                keyColumn: "Id",
                keyValue: 4,
                column: "RecordStatus",
                value: 0);
        }
    }
}
