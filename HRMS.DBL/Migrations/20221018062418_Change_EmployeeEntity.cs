using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    public partial class Change_EmployeeEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeStatus_EmployeeStatusesId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_EmployeeStatusesId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "EmployeeStatusesId",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Status",
                table: "Employees",
                column: "Status");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeStatus_Status",
                table: "Employees",
                column: "Status",
                principalTable: "EmployeeStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_EmployeeStatus_Status",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_Status",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeStatusesId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmployeeStatusesId",
                table: "Employees",
                column: "EmployeeStatusesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_EmployeeStatus_EmployeeStatusesId",
                table: "Employees",
                column: "EmployeeStatusesId",
                principalTable: "EmployeeStatus",
                principalColumn: "Id");
        }
    }
}
