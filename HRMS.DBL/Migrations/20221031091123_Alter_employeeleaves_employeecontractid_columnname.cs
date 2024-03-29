using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    public partial class Alter_employeeleaves_employeecontractid_columnname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeLeaves_EmployeeContracts_EmployeeContractID",
                table: "EmployeeLeaves");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeLeaves_LeaveTypes_LeaveTypeID",
                table: "EmployeeLeaves");

            migrationBuilder.RenameColumn(
                name: "LeaveTypeID",
                table: "EmployeeLeaves",
                newName: "LeaveTypeId");

            migrationBuilder.RenameColumn(
                name: "EmployeeContractID",
                table: "EmployeeLeaves",
                newName: "EmployeeContractId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeLeaves_LeaveTypeID",
                table: "EmployeeLeaves",
                newName: "IX_EmployeeLeaves_LeaveTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeLeaves_EmployeeContractID",
                table: "EmployeeLeaves",
                newName: "IX_EmployeeLeaves_EmployeeContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeLeaves_EmployeeContracts_EmployeeContractId",
                table: "EmployeeLeaves",
                column: "EmployeeContractId",
                principalTable: "EmployeeContracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeLeaves_LeaveTypes_LeaveTypeId",
                table: "EmployeeLeaves",
                column: "LeaveTypeId",
                principalTable: "LeaveTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeLeaves_EmployeeContracts_EmployeeContractId",
                table: "EmployeeLeaves");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeLeaves_LeaveTypes_LeaveTypeId",
                table: "EmployeeLeaves");

            migrationBuilder.RenameColumn(
                name: "LeaveTypeId",
                table: "EmployeeLeaves",
                newName: "LeaveTypeID");

            migrationBuilder.RenameColumn(
                name: "EmployeeContractId",
                table: "EmployeeLeaves",
                newName: "EmployeeContractID");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeLeaves_LeaveTypeId",
                table: "EmployeeLeaves",
                newName: "IX_EmployeeLeaves_LeaveTypeID");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeLeaves_EmployeeContractId",
                table: "EmployeeLeaves",
                newName: "IX_EmployeeLeaves_EmployeeContractID");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeLeaves_EmployeeContracts_EmployeeContractID",
                table: "EmployeeLeaves",
                column: "EmployeeContractID",
                principalTable: "EmployeeContracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeLeaves_LeaveTypes_LeaveTypeID",
                table: "EmployeeLeaves",
                column: "LeaveTypeID",
                principalTable: "LeaveTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
