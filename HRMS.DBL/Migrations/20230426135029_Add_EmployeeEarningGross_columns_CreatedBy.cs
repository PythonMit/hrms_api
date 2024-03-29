using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeEarningGrosscolumnsCreatedBy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "EmployeeEarningGross",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEarningGross_CreatedBy",
                table: "EmployeeEarningGross",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeEarningGross_Employees_CreatedBy",
                table: "EmployeeEarningGross",
                column: "CreatedBy",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeEarningGross_Employees_CreatedBy",
                table: "EmployeeEarningGross");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeEarningGross_CreatedBy",
                table: "EmployeeEarningGross");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "EmployeeEarningGross");
        }
    }
}
