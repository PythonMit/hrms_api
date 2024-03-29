using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    public partial class Alter_EmployeeDetails_EmployeeId_ColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeDetails_Employees_EmployeeID",
                table: "EmployeeDetails");

            migrationBuilder.RenameColumn(
                name: "EmployeeID",
                table: "EmployeeDetails",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeDetails_EmployeeID",
                table: "EmployeeDetails",
                newName: "IX_EmployeeDetails_EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeDetails_Employees_EmployeeId",
                table: "EmployeeDetails",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeDetails_Employees_EmployeeId",
                table: "EmployeeDetails");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "EmployeeDetails",
                newName: "EmployeeID");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeDetails_EmployeeId",
                table: "EmployeeDetails",
                newName: "IX_EmployeeDetails_EmployeeID");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeDetails_Employees_EmployeeID",
                table: "EmployeeDetails",
                column: "EmployeeID",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
