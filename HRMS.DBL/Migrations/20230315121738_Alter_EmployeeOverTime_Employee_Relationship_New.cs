using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.DBL.Migrations
{
    /// <inheritdoc />
    public partial class AlterEmployeeOverTimeEmployeeRelationshipNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_EmployeeOverTimes_ApprovedBy",
                table: "EmployeeOverTimes",
                column: "ApprovedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeOverTimes_Employees_ApprovedBy",
                table: "EmployeeOverTimes",
                column: "ApprovedBy",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeOverTimes_Employees_ApprovedBy",
                table: "EmployeeOverTimes");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeOverTimes_ApprovedBy",
                table: "EmployeeOverTimes");
        }
    }
}
