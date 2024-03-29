using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HRMS.DBL.Migrations
{
    /// <inheritdoc />
    public partial class AddDesignationTypedata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeLeaveTransaction_EmployeeContracts_EmployeeContractId",
                table: "EmployeeLeaveTransaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeLeaveTransaction",
                table: "EmployeeLeaveTransaction");

            migrationBuilder.RenameTable(
                name: "EmployeeLeaveTransaction",
                newName: "EmployeeLeaveTransactions");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeLeaveTransaction_EmployeeContractId",
                table: "EmployeeLeaveTransactions",
                newName: "IX_EmployeeLeaveTransactions_EmployeeContractId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeLeaveTransactions",
                table: "EmployeeLeaveTransactions",
                column: "Id");

            migrationBuilder.InsertData(
                table: "DesignationTypes",
                columns: new[] { "Id", "CreatedDateTimeUtc", "Description", "Name", "Order", "RecordStatus", "UpdatedDateTimeUtc" },
                values: new object[,]
                {
                    { 24, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Project Trainee", 24, 1, null },
                    { 25, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Project Trainee", 25, 1, null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeLeaveTransactions_EmployeeContracts_EmployeeContractId",
                table: "EmployeeLeaveTransactions",
                column: "EmployeeContractId",
                principalTable: "EmployeeContracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeLeaveTransactions_EmployeeContracts_EmployeeContractId",
                table: "EmployeeLeaveTransactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeLeaveTransactions",
                table: "EmployeeLeaveTransactions");

            migrationBuilder.DeleteData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "DesignationTypes",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.RenameTable(
                name: "EmployeeLeaveTransactions",
                newName: "EmployeeLeaveTransaction");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeLeaveTransactions_EmployeeContractId",
                table: "EmployeeLeaveTransaction",
                newName: "IX_EmployeeLeaveTransaction_EmployeeContractId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeLeaveTransaction",
                table: "EmployeeLeaveTransaction",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeLeaveTransaction_EmployeeContracts_EmployeeContractId",
                table: "EmployeeLeaveTransaction",
                column: "EmployeeContractId",
                principalTable: "EmployeeContracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
