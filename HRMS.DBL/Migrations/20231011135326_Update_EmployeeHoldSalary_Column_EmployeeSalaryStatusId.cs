using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEmployeeHoldSalaryColumnEmployeeSalaryStatusId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "EmployeeHoldSalary",
                newName: "EmployeeSalaryStatusId");

            migrationBuilder.UpdateData(
                table: "EmployeeEarningGrossStatus",
                keyColumn: "Id",
                keyValue: 1,
                column: "StatusType",
                value: "In Process");

            migrationBuilder.InsertData(
                table: "EmployeeSalaryStatus",
                columns: new[] { "Id", "CreatedDateTimeUtc", "Description", "RecordStatus", "StatusType", "UpdatedDateTimeUtc" },
                values: new object[] { 4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, "Partial Paid", null });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHoldSalary_EmployeeSalaryStatusId",
                table: "EmployeeHoldSalary",
                column: "EmployeeSalaryStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeHoldSalary_EmployeeSalaryStatus_EmployeeSalaryStatusId",
                table: "EmployeeHoldSalary",
                column: "EmployeeSalaryStatusId",
                principalTable: "EmployeeSalaryStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeHoldSalary_EmployeeSalaryStatus_EmployeeSalaryStatusId",
                table: "EmployeeHoldSalary");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeHoldSalary_EmployeeSalaryStatusId",
                table: "EmployeeHoldSalary");

            migrationBuilder.DeleteData(
                table: "EmployeeSalaryStatus",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.RenameColumn(
                name: "EmployeeSalaryStatusId",
                table: "EmployeeHoldSalary",
                newName: "Status");

            migrationBuilder.UpdateData(
                table: "EmployeeEarningGrossStatus",
                keyColumn: "Id",
                keyValue: 1,
                column: "StatusType",
                value: "InProcess");
        }
    }
}
